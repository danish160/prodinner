utils = function ($) {
    function gvfilter(gv, regs, props) {
        var regs1 = regs;

        if (gv.Header) {
            gv.Header.Collapsed = false;
            gv.Header.IgnorePersistence = true;
            if (gv.Header.Item) {
                regs1 = $.grep(regs, function (r) {
                    return !any(gv.Header.Item, props, r.test.bind(r));
                });

                if (!regs1.length) return 1;
            }
        }

        if (gv.Groups && gv.Groups.length) {
            var fgroups = [];
            $.each(gv.Groups, function (i, val) {
                if (gvfilter(val, regs1, props)) {
                    fgroups.push(val);
                }
            });

            gv.Groups = fgroups;

            return fgroups.length;

        } else if (gv.Items && gv.Items.length) {
            var fitems = [];
            $.each(gv.Items, function (i, val) {
                var count = $.grep(regs1, function (r) {
                    return any(val, props, r.test.bind(r));
                }).length;

                if (count == regs.length) fitems.push(val);
            });
            gv.Items = fitems;
            return fitems.length;
        }

        return 0;
    }

    function any(o, props, f) {
        
        for (var i = 0; i < props.length; i++) {
            var val = o[props[i]];
            if (val && f(val)) return 1;
        }
        return 0;
    }
    return {
        // grid crud
        itemDeleted: function (gridId) {
            return function (res) {
                var $grid = $("#" + gridId);
                var key = $grid.data('o').k;
                $grid.data('api').select(res[key])[0].fadeOut(500, function () {
                    $(this).remove();
                    if (!$grid.find('.awe-row').length) $grid.data('api').load();
                });
            };
        },

        itemEdited: function (gridId) {
            return function (item) {
                var $grid = $('#' + gridId);
                var api = $grid.data('api');
                var key = $grid.data('o').k;
                var xhr = api.update(item[key]);
                $.when(xhr).done(function () {
                    var $row = api.select(item[key])[0];
                    var altcl = $row.hasClass("awe-alt") ? "awe-alt" : "";
                    $row.switchClass(altcl, "awe-changing", 1).switchClass("awe-changing", altcl, 1000);
                });
            };
        },

        itemCreated: function (gridId) {
            return function (item) {
                var $grid = $("#" + gridId);
                var $row = $grid.data('api').renderRow(item);
                $grid.find(".awe-content .awe-tbody").prepend($row);
                $row.addClass("awe-changing").removeClass("awe-changing", 1000);
            };
        },

        // grid nest
        loadNestPopup: function (popupName) {
            return function (row, nestrow, cell) {
                var key = $(row).closest('.awe-grid').data('o').k;
                var id = $(row).data('model')[key];
                var params = {};
                params[key] = id;
                awe.open(popupName, { params: params, tag: { cont: cell } });
                cell.on('aweclose', function (e) {
                    if ($(e.target).is(cell.find('.awe-popup:first')))
                        nestrow.data('api').close();
                });
            };
        },

        // ajaxlist crud
        itemCreatedAlTbl: function (listId) {
            return function (o) {
                $('#' + listId).parent().find('tbody').prepend(o.Content);
            };
        },

        itemEditedAl: function (listId, key, func) {
            return function (o) {
                $('#' + listId).find('[data-val="' + o[key] + '"]').fadeOut(300, function () {
                    $(this).after($.trim(o.Content)).remove();
                    if (func) func();
                });
            };
        },

        itemDeletedAl: function (listId, key) {
            return function (o) {
                $('#' + listId).find('[data-val="' + o[key] + '"]').fadeOut(300, function () { $(this).remove(); });
            };
        },

        itemCreatedAl: function (listId) {
            return function (o) {
                $('#' + listId).prepend($.trim(o.Content));
            };
        },

        // lookup crud, the InitPopupForm helpers must be called in the SearchForm view
        lookupEdited: function (key) {
            return function (o) {
                this.f.closest('.awe-popup').find('[data-val="' + o[key] + '"]').fadeOut(300, function () { $(this).after(o.Content).remove(); });
            };
        },

        lookupDeleted: function (key) {
            return function (o) {
                this.f.closest('.awe-popup').find('[data-val="' + o[key] + '"]').fadeOut(300, function () { $(this).remove(); });
            };
        },

        lookupCreated: function (o) {
            this.f.closest('.awe-popup').find('.awe-srl').prepend(o.Content);
        },

        lookupTblCreated: function (o) {
            this.f.closest('.awe-popup').find('tbody').prepend(o.Content);
        },

        scheduler: function (id, popupName) {
            var $grid = $('#' + id);
            var $sched = $grid.closest('.scheduler');
            var api = $grid.data('api');
            var $viewType = $sched.find('.viewType .awe-val');

            $sched.find('.prevbtn').click(function () {
                api.load({ oparams: { cmd: 'prev' } });
            });

            $sched.find('.nextbtn').click(function () {
                api.load({ oparams: { cmd: 'next' } });
            });

            $sched.find('.todaybtn').click(function () {
                api.load({ oparams: { cmd: 'today' } });
            });

            $grid
                .on('aweload', function (e, data) {
                    var tag = data.Tag;

                    if (tag.View == 'Agenda' || tag.View == 'Month') {
                        $('.schedBotBar').hide();
                    }
                    else
                        $('.schedBotBar').show();

                    if ($viewType.val() != tag.View) {
                        $viewType.val(tag.View).data('api').render();
                    }

                    $sched.find('.schedDate .awe-val').val(tag.Date);
                    $sched.find('.dateLabel').html(tag.DateLabel);
                })
                .on('click', '.eventTitle', function () {
                    awe.open('edit' + popupName, { params: { id: $(this).parent().data('id') } });
                })
                .on('dblclick', '.schedEvent', function (e) {
                    if (!$(e.target).is('.delEvent'))
                        awe.open('edit' + popupName, { params: { id: $(this).data('id') } });
                })
                .on('click', '.delEvent', function () {
                    awe.open('delete' + popupName, { params: { id: $(this).closest('.schedEvent').data('id') } });
                })
                .on('dblclick', '.timePart', function (e) {
                    if ($(e.target).is('.timePart'))
                        awe.open('create' + popupName, { params: { ticks: $(this).data('ticks'), allDay: $(this).data('allday') } });
                })
                .on('click', '.day', function (e) {
                    if ($(e.target).is('.day')) {
                        api.load({ oparams: { viewType: 'Day', date: $(this).data('date') } });
                    }
                });
        },

        //misc
        refreshGrid: function (gridId) {
            return function () {
                $("#" + gridId).data('api').load();
            };
        },

        getMinutesOffset: function () {
            return { minutesOffset: new Date().getTimezoneOffset() };
        },

        //used for .DataFunc, items is KeyContent[]
        getItems: function (items) {
            return function () {
                return items;
            };
        },

        //http://stackoverflow.com/a/1186309/112100
        serializeObj: function (a) {
            var o = {};
            $.each(a, function () {
                if (o[this.name] !== undefined) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        },

        init: function (dateFormat, isMobileOrTablet) {

            if (isMobileOrTablet) {
                awe.ff = function (o) {
                    o.p.d.find(':tabbable').blur();//override jQueryUI dialog autofocus
                };
            }

            //by default jquery.validate doesn't validate hidden inputs
            if ($.validator) {
                $.validator.setDefaults({
                    ignore: []
                });

                setjQueryValidateDateFormat(dateFormat);

                $(function () {
                    //parsing the unobtrusive attributes when we get content via ajax
                    $(document).ajaxComplete(function () {
                        $.validator.unobtrusive.parse(document);
                    });
                });
            }

            utils.setPopup();

            $(function () {
                utils.consistentSearchTxt();
                $(document).ajaxComplete(utils.consistentSearchTxt);
            });

            //remove locaStorage keys from older versions of awesome; you can modify  ppk (awe.ppk = "myapp1_"), current value is "awe2_"
            try {
                for (var key in localStorage) {
                    if (key.indexOf("awe") == 0) {
                        if (key.indexOf(awe.ppk) != 0) {
                            localStorage.removeItem(key);
                        }
                    }
                }
            } catch (err) { }

            function setjQueryValidateDateFormat(format) {
                //setting the correct date format for jquery.validate
                $.validator.addMethod(
                    'date',
                    function (value, element, params) {
                        if (this.optional(element)) {
                            return true;
                        };
                        var result = false;
                        try {
                            $.datepicker.parseDate(format, value);
                            result = true;
                        } catch (err2) {
                            result = false;
                        }
                        return result;
                    },
                    ''
                );
            }
        },

        // on ie hitting enter doesn't trigger change, 
        // all searchtxt inputs will trigger change on enter in all browsers
        consistentSearchTxt: function () {
            $('.searchtxt').each(function () {
                if ($(this).data('searchtxth') != 1)
                    $(this).data('searchtxth', 1)
                        .data('myval', $(this).val())
                        .on('change', function (e) {
                            if ($(this).val() != $(this).data('myval')) {
                                $(this).data('myval', $(this).val());
                            } else {
                                e.stopImmediatePropagation();
                            }
                        })
                        .on('keyup', function (e) {
                            if (e.which == 13) {
                                e.preventDefault();
                                if ($(this).val() != $(this).data('myval')) {

                                    $(this).change();
                                }
                            }
                        });
            });
        },
        setPopup: function () {
            var jqueryUIpopup = awe.popup;

            awe.popup = function (o) {
                if (o.tag && o.tag.DropdownPopup) {
                    return awem.dropdownPopup(o);
                } else if (o.tag && o.tag.Inline) {
                    return awem.inlinePopup(o);
                } else {
                    return jqueryUIpopup(o);
                }
            };
        },
        gfilter: function (gid, props, str) {
            var $grid = $('#' + gid);
            var lrs = $grid.data('o').lrs;
            var clrs = $.extend(true, {}, lrs);

            if (str) {

                var regs = $.map(str.trim().split(' '), function (element) {
                    return new RegExp(element, "i");
                });

                gvfilter(clrs.Data, regs, props);
            }

            $grid.data('o').lrs = clrs;
            $grid.data('api').render();
            $grid.data('o').lrs = lrs;
        }
    };
}(jQuery);
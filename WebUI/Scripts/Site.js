function documentReady(root, controller, action) {
    //bind the window min-height to window size
    adjustLayout();
    $(window).on('resize', adjustLayout);

    initDog(root, controller, action);

    // make server side validation message html be the same as client side (almost)
    $(document).ajaxComplete(function () {
        $('.field-validation-error').each(function () {
            var $this = $(this);
            if (!$this.children().first().is('span')) {
                var $msg = $('<span/>').html($this.html());
                $(this).html($msg);
            }
        });
    });
    
    $('#chTheme').change(function () {
        var theme = $('#chTheme').val();

        $('#aweStyle').attr('href', root + "Content/themes/" + theme + "/AwesomeMvc.css?v=100");
        $('#modsStyle').attr('href', root + "Content/themes/" + theme + "/mods.css");
        $.post(root + "Settings/Change", { theme: theme }, function () {
            setTimeout(function () {
                $('.awe-grid').each(
                    function () {
                        $(this).data('api').lay();
                    });
            }, 500);
        });
    });

    $('#chLang').change(function () {
        console.log('new val', $(this).val());
        $.post(root + "Settings/ChangeLang", { l: $(this).val() }, function () {
            location.reload();
        });
    });
    
    awe.err = function (o, xhr, textStatus, errorThrown) {
        var msg = "unexpected error occured";
        if (xhr) {
            msg = xhr.responseText;
        }
        var btnHide = $('<button type="button"> hide </button>').click(function () {
            $(this).parent().remove();
        });

        var c = $('<div/>').html(msg).append(btnHide);

        if (o.p && o.p.isOpen) {
            o.p.d.prepend(c);
        } else if (o.f) {
            o.f.html(c);
        } else if (o.d) {
            o.d.after(c);
        } else $('body').prepend(c);
    };
}

function passchanged(o) {
    $("<div> password for " + o.Login + " was successfuly changed </div>").dialog();
}

//adjust pageszie of meals multilookup depending on available width
function getMealsLookupPageSize() {
    var w = $('[id$="Meals-awepw"]:visible .awe-ajaxlist').width();
    var mealWidth = 212;
    if (w < mealWidth) mealWidth = w - 20;
    var countPerRow = parseInt(w / mealWidth, 10);
    var rows = parseInt(10 / countPerRow, 10);
    var pageSize = Math.max(rows, 1) * countPerRow;

    return { pageSize: pageSize };
}

function adjustLayout() {
    $("#main").css("min-height", ($(window).height() - 120) + "px");
}

function initDog(root, controller, action) {
    var dog = Dog(root, controller, action);
    dog.load();

    function setText() {
        if (dog.state() != "hidden") {
            $('#chDog').html('hide dog');
        } else {
            $('#chDog').html('show dog');
        }
    }

    setText();

    $('#chDog').click(function () {
        dog.state() != "hidden" ? dog.hide() : dog.show();
        setText();
    });
}

function Dog(root, controller, action) {
    var state = "visible";
    var h;
    var $dog = $('#dogh');
    var $tip = $('#tip');
    var $tipContent = $('#tipcontent');
    
    function load() {
        var s = storg.getItem("dogstate");
        if (s) state = s;
        changeStateTo(state);
        $dog.click(function () { changeStateTo(state == "visible" ? "visiblenotip" : "visible"); });
        $tip.click(function () {
            clearTimeout(h);
            tell();
        });
        
        $dog.draggable({
            drag: function () {
                var dl = parseFloat($dog.css('left'));
                var dt = parseFloat($dog.css('top'));
                $tip.css('left', (dl - 115) + "px").css('top', (dt - 100) + 'px');
            }
        });
    }
    
    function changeStateTo(ns) {
        states[ns]();
        state = ns;
        storg.setItem("dogstate", state);
    }
    
    var times = 0;
    function tell() {
        if (state == "visible")
            $.post(root + "Dog/Tell", { c: controller, a: action },
            function (d) {
                $tipContent.html(d.o);
                times++;
                if (times < 10)
                    h = setTimeout(tell, Math.random() * 8000 + 5000);
            });
    }

    var states = {
        visible: function () {
            $dog.show();
            $tip.show();
            h = setTimeout(tell, 5000);
        },
        visiblenotip: function () {
            $dog.show();
            $tip.hide();
            clearTimeout(h);
        },
        hidden: function () {
            $dog.hide();
            $tip.hide();
            clearTimeout(h);
        }
    };

    return {
        load: load,
        hide: function () {
            changeStateTo("hidden");
        },
        show: function () {
            changeStateTo("visible");
        },
        state: function () { return state; }
    };
}

//used by the Dog
storg = function () {
    if (localStorage) return localStorage;
    var list = {};
    return {
        setItem: function (key, value) {
            list[key] = value;
        },
        getItem: function (key) {
            return list[key];
        }
    };
}();

//google analytics
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-27119754-1']);
_gaq.push(['_setDomainName', 'aspnetawesome.com']);
_gaq.push(['_trackPageview']);

(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
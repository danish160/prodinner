// utils added in prodinner
(function ($) {
    utils.deleteFormat = function(popupName, gridId) {
        return function(model) {
            if (model.IsDeleted) {
                return "<button type='button' class='awe-btn' onclick=\"utils.restore('" + gridId + "'," + model.Id + ")\"><span class='ico-restore'></span></button>";
            }

            return "<button type='button' class='awe-btn' onclick=\"awe.open('" + popupName + "', { params:{ id: " + model.Id + " } })\"><span class='ico-del'></span></button>";
        };
    };

    utils.restore = function(gridId, id) {
        var api = $('#' + gridId).data('api');
        var xhr = api.update(id, { restore: true });

        $.when(xhr).done(function() {
            var $row = api.select(id)[0];
            var altcl = $row.hasClass("awe-alt") ? "awe-alt" : "";
            $row.switchClass(altcl, "awe-changing", 1).switchClass("awe-changing", altcl, 1000);
        });
    };

    utils.lookupRestored = function(listId, key, func) {
        return function(o) {
            $('#' + listId).find('[data-val="' + o[key] + '"]').fadeOut(300, function() {
                $(this).after($.trim(o.Content)).remove();
                if (func) func();
            });
        };
    };
})(jQuery);
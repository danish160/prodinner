<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.ProDinner.Core.Model.Meal>" %>
<div style="padding: 0.5em 0;display:block;" class="awe-morebtn">
    <form id="file_upload" action="<%=Url.Action("upload") %>" method="post" enctype="multipart/form-data">
    <input type="file" name="file" />
    <button>Upload</button>
    <div>
        drag and drop here an image from your desktop or click here and select</div>
    </form>
    <table id="files">
    </table>
</div>
<div class="fl" style="width: 210px;">
    <div class="thumbhead ui-widget-header">
        picture
    </div>
    <div class="fl thumb">
        <img id="currentPicture" src='<%=Url.Content("~/pictures/meals/" + (string.IsNullOrEmpty(Model.Picture) ? "0.jpg": Model.Picture)) %>'
            alt="picture" />
    </div>
    <div class="fl" style="margin: 0.6em 0">
        <form class="cropform" method="post" action='<%=Url.Action("crop") %>'>
        <input type="hidden" id="x" name="x" />
        <input type="hidden" id="y" name="y" />
        <input type="hidden" id="w" name="w" />
        <input type="hidden" id="h" name="h" />
        <input type="hidden" id="id" name="id" value="<%=Model.Id %>" />
        <input type="hidden" id="filename" name="filename" />
        <button type="submit" style="width: 200px;" class="awe-btn">save image</button>
        </form>
    </div>
    <div class='thumbhead ui-widget-header'>
        preview
    </div>
    <div class="fl thumb" style="overflow: hidden;">
        <img src="<%=Url.Content(@"~/pictures/meals/0.jpg")%>" id="preview" alt="" />
    </div>
</div>
<div class="cropbox">
    <img id="cropbox" src="<%=Url.Content(@"~/pictures/meals/0.jpg")%>" alt="" />
</div>
<div style="float: right; width: 130px;">
    <ol>
        <li>upload a picture
            <br />
            (drag and drop to this big button above
            <br />
            or just click it)</li>
        <li>select the box to crop</li>
        <li>click "save image"</li>
        <li>hit 'esc' or click on the 'x'</li>
    </ol>
</div>
<script type="text/javascript">
    function updatePic(o) {
        $('#currentPicture').attr('src', '<%=Url.Content("~/pictures/meals/") %>' + o.name + '?' + Math.random());
        picChanged = true;
    }
    
    function check() {
        if ($('#filename').val().length == 0) {
            alert('select an image first');
            return false;
        }
        return true;
    }
    
    var picChanged = false;
    $(function () {
        $('#currentPicture').closest('.awe-popup').on('aweclose', function () {
            if (picChanged) {
                picChanged = false;
                updateMeal($('#id').val()); // js func from Meals/Index.cshtml
            }
        });
        
        $('#file_upload').fileUploadUI({
            uploadTable: $('#files'),
            downloadTable: $('#files'),
            buildUploadRow: function (files, index) {
                return $('<tr><td>' + files[index].name + '<\/td>' +
                    '<td class="file_upload_progress"><div><\/div><\/td>' +
                    '<td class="file_upload_cancel">' +
                    '<button class="ui-state-default ui-corner-all" title="Cancel">' +
                    '<span class="ui-icon ui-icon-cancel">Cancel<\/span>' +
                    '<\/button><\/td><\/tr>');
            },
            buildDownloadRow: function (file) {
                $('#cropbox, #preview').attr('src', '<%=Url.Content("~/pictures/temp/") %>' + file.name);
                $('#filename').val(file.name);
                $('#cropbox').width(file.w).height(file.h);
                if (api) api.destroy();
                var dw = file.w;
                var dh = file.h;
                setTimeout(function() {
                    docrop(dh, dw);
                }, 200);
                
                return $('');
            },
            addNode: function (parentNode, node, callBack) {
                parentNode.empty();
                parentNode.append(node);
                if (typeof callBack === 'function') {
                    callBack();
                }
            }
        });
    });

    var api;
    function docrop(dh, dw) {
        api = $.Jcrop('#cropbox', {
            setSelect: [0, 0, 200, 150],
            onChange: function (coords) { showPreview(coords, dh, dw); },
            onSelect: function (coords) { showPreview(coords, dh, dw); },
            aspectRatio: 1.333
        });
    }

    function updateCoords(c) {
        $('#x').val(Math.round(c.x));
        $('#y').val(Math.round(c.y));
        $('#w').val(Math.round(c.w));
        $('#h').val(Math.round(c.h));
    }

    function showPreview(coords, dh, dw) {
        updateCoords(coords);

        if (parseInt(coords.w) > 0) {
            
            var rx = 200 / coords.w;
            var ry = 150 / coords.h;
            $('#preview').css({
                width: Math.round(rx * dw) + 'px',
                height: Math.round(ry * dh) + 'px',
                marginLeft: '-' + Math.round(rx * coords.x) + 'px',
                marginTop: '-' + Math.round(ry * coords.y) + 'px'
            });
        }
    }
</script>
<%:Html.Awe().Form().FormClass("cropform").Success("updatePic").BeforeSubmit("check") %>

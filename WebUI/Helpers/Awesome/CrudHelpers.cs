using System.Web;
using System.Web.Mvc;

using Omu.AwesomeMvc;

namespace Omu.ProDinner.WebUI.Helpers.Awesome
{
    public static class CrudHelpers
    {
        /*beging*/
        public static IHtmlString InitCrudPopupsForGrid<T>(this HtmlHelper<T> html, string gridId, string crudController, int createPopupHeight = 400, bool fullScreen = false)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var result =
            html.Awe()
                .InitPopupForm()
                .Name("create" + gridId)
                .Group(gridId)
                .Height(createPopupHeight)
                .Url(url.Action("Create", crudController))
                .Success("utils.itemCreated('" + gridId + "')")
                .Fullscreen(fullScreen)
                .ToString()

            + html.Awe()
                  .InitPopupForm()
                  .Name("edit" + gridId)
                  .Group(gridId)
                  .Height(createPopupHeight)
                  .Url(url.Action("Edit", crudController))
                  .Fullscreen(fullScreen)
                  .Success("utils.itemEdited('" + gridId + "')")

            + html.Awe()
                  .InitPopupForm()
                  .Name("delete" + gridId)
                  .Group(gridId)
                  .Url(url.Action("Delete", crudController))
                  .Success("utils.itemDeleted('" + gridId + "')")
                  .Parameter("gridId", gridId) // used to call grid.api.select and emphasize the row
                  .Height(200)
                  .Modal(true);

            return new MvcHtmlString(result);
        }
        /*endg*/

        /*beginal*/
        public static IHtmlString InitCrudPopupsForAjaxList<T>(
            this HtmlHelper<T> html,
            string ajaxListId,
            string controller, 
            string key, 
            string popupName, 
            string afterUpdateFunc = null, 
            bool fullScreen = false)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var editSuccessFunc = afterUpdateFunc == null
                                      ? string.Format("utils.itemEditedAl('{0}', '{1}')", ajaxListId, key)
                                      : string.Format(
                                          "utils.itemEditedAl('{0}', '{1}', {2})", ajaxListId, key, afterUpdateFunc);
            var result =
                html.Awe()
                    .InitPopupForm()
                    .Name("create" + popupName)
                    .Url(url.Action("Create", controller))
                    .Height(330)
                    .Success("utils.itemCreatedAl('" + ajaxListId + "')")
                    .Group(ajaxListId)
                    .Parameter("usingAjaxList", true)
                    .Fullscreen(fullScreen)
                    .Title("create item")
                    .ToString()

                + html.Awe()
                      .InitPopupForm()
                      .Name("edit" + popupName)
                      .Url(url.Action("Edit", controller))
                      .Height(330)
                      .Parameter("usingAjaxList", true)
                      .Success(editSuccessFunc)
                      .Group(ajaxListId)
                      .Fullscreen(fullScreen)
                      .Title("edit item")

                + html.Awe()
                      .InitPopupForm()
                      .Name("delete" + popupName)
                      .Url(url.Action("Delete", controller))
                      .Success("utils.itemDeletedAl('" + ajaxListId + "', '" + key + "')")
                      .Group(ajaxListId)
                      .OkText("Yes")
                      .CancelText("No")
                      .Height(200)
                      .Title("delete item")
                      
                + html.Awe()
                      .InitCall("restore" + popupName)
                      .Url(url.Action("Restore", controller))
                      .Success(editSuccessFunc);

            return new MvcHtmlString(result);
        }
        /*endal*/
    }
}
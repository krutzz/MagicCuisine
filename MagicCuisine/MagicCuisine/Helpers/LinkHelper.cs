using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MagicCuisine.Helpers
{
    public static class LinkHelper
    {
        public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "selected")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
            {
                viewContext = html.ViewContext.ParentActionViewContext;
            }

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
            {
                actions = currentAction;
            }

            if (String.IsNullOrEmpty(controllers))
            {
                controllers = currentController;
            }

            string[] acceptedActions = actions.ToLower().Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.ToLower().Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction.ToLower()) && acceptedControllers.Contains(currentController.ToLower()) ?
                cssClass : String.Empty;
        }

    public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper,
    string linkText, string action, string controller,
    object routeValues, object htmlAttributes, string imageSrc)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imageSrc));
            var anchor = new TagBuilder("a")
            { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());
        }
    }
}


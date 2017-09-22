using System;
using System.Linq;
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
    }
}


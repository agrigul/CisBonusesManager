using System.Data;
using System.Net;
using System.Web.Mvc;

namespace Web.Controllers.Attributes
{
    /// <summary>
    /// Fitler to catch all exception from business logic layer and send formatted json results
    /// </summary>
    public class AjaxErrorFilterAttribute : HandleErrorAttribute
    {

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
        public override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null)
            {
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = false; // supress IIS 500 error page.
                filterContext.HttpContext.Response.AddHeader("Content-Type", "application/json");
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                string errorMessage;
                if (filterContext.Exception is ProviderIncompatibleException)
                {
                    errorMessage = string.Format("Possible you have no permission to access database. {0}",
                                                 filterContext.Exception.Message);
                }
                else
                {
                    errorMessage = string.Format("Operation failed. {0}",
                                                 filterContext.Exception.Message);
                }

                filterContext.Result = new JsonResult
                                           {
                                               JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                                               Data = new { Error = errorMessage }
                                           };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}
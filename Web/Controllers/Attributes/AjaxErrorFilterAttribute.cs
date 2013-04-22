using System.Data;
using System.Net;
using System.Web.Mvc;

namespace Web.Controllers.Attributes
{
    /// <summary>
    /// Fitler catches all exception from business logic layer and send formatted json results
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
                // supress IIS to replace custom error 500 with it's error page .
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = false; 
                filterContext.HttpContext.Response.AddHeader("Content-Type", "application/json");
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                string errorMessage;

                // database throw ProviderIncompatibleException if user's credentials are wrong.
                if (filterContext.Exception is EntityException)
                {
                    errorMessage = string.Format("Possible you have no access permission to database or entered wrong login and password.");
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
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Repository;

namespace Web.Filters
{
    /// <summary>
    /// A membership attribute for security.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);

            RedirectIfSessionExpired(filterContext);
        }

        /// <summary>
        /// Redirects if session expired.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        private static void RedirectIfSessionExpired(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            var user = SessionRepository.GetUserCredentials();

            if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession))
            {
                //send them off to the login page
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Content("~/Account/Login");
                session.RemoveAll();
                session.Clear();
                session.Abandon();
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
                SessionRepository.ClearUser();
            }
        }

        private class SimpleMembershipInitializer
        {
        }
    }
}

using System.Web.Mvc;
using System.Web.Security;
using Web.Infrastructure.Repository;
using Web.Models.ValueObjects;

namespace Web.Controllers
{
    /// <summary>
    /// Class AccountController
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                SessionRepository.SetUserCredentials(user);
                FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            return View(user);
        }
        
        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult LogOff()
        {
            if (SessionRepository.GetUserCredentials() != null)
            {
                SessionRepository.ClearUser();
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        #region Helpers

        /// <summary>
        /// Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Bonuses");
            }
        }


        #endregion Helpers
    }
}

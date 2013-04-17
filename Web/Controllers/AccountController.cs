using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Web.Filters;
using Web.Infrastructure.Repository;
using Web.Models;
using Web.Models.ValueObjects;
using WebMatrix.WebData;

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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel user, string returnUrl)
        {
            if (ModelState.IsValid)
                //&& WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                SessionRepository.SetUserCredentials(user);
                FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            return View(user);
        }

        // POST: /Account/LogOff

        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            // WebSecurity.Logout();
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

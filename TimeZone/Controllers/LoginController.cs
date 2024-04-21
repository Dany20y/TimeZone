using Time_Zone.BusinessLogic.Interfaces;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Time_Zone.BusinessLogic;
using Time_Zone.Models;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Entities.User.Global;
using System.Web.Security;

namespace Time_Zone.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult Login()
        {
            return View();
        }


        // GET: Login
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userLogin login)
        {

            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginDataTime = DateTime.Now,
                };

                var userLogin = _session.UserLogin(data);

                if (userLogin.Status)
                {
                    FormsAuthentication.SetAuthCookie(login.Credential, false);
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Credential"] = login.Credential;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Login failed, add error message to ModelState
                    ViewBag.ErrorMessage = userLogin.StatusMessage;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDataTime = DateTime.Now,
                };

                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
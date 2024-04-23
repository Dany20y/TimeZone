using System;
using System.Web.Mvc;
using Time_Zone.Models;
using System.Web.Security;
using System.Web;
using Time_Zone.BusinessLogic;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using Domain.Entities.User;

namespace App.Controllers
{
    public class LogInPageController : Controller
    {
        private readonly ISession _session;

        public LogInPageController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(userLogin login)
        {
            HttpContext.Session["UserProfile"] = login;

            if (ModelState.IsValid)
            {
                var data = Mapper.Map<ULoginData>(login);
                data.LoginIp = Request.UserHostAddress.ToString();
                data.LoginDateTime = DateTime.Now;

                ActionStatus resp = _session.UserLogin(data);
                if (resp.Status)
                {
                    FormsAuthentication.SetAuthCookie(data.Email, false);
                    HttpCookie cookie = _session.GenCookie(data.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Username"] = data.Email;

                    // Check if the user is an admin
                    if (resp.StatusMessage == "Admin")
                    {
                        // Set user role to "Admin"
                        FormsAuthentication.SetAuthCookie(data.Email, false);
                        Session["UserRole"] = "Admin";

                        // Redirect to admin dashboard or profile
                        return RedirectToAction("Admin", "Admin");
                    }
                    else
                    {
                        // Set user role to "User"
                        FormsAuthentication.SetAuthCookie(data.Email, false);
                        Session["UserRole"] = "User";

                        // Redirect to user dashboard or profile
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Login failed, display error message
                    ViewBag.ErrorMessage = resp.StatusMessage;
                    return View(login);
                }
            }
            return View(login);
        }

    }
}

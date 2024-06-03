using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Time_Zome.BussinesLogic;
using Time_Zone.BussinessLogic;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Enums;

namespace Time_Zome.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {

            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                _session.LogoutUserByCookie(token, HttpContext);
            }

            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("About", "Home");
            }

            // Initialize the login details
            UserLogin data = new UserLogin
            {
                Credential = login.Credential,
                Password = login.Password,
                LoginIp = Request.UserHostAddress,
                LoginDateTime = DateTime.Now,
            };

            // Attempt to log the user in
            var userLogin = _session.UserLogin(data);
            if (!userLogin.Status)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return RedirectToAction("About", "Home");
            }

            // Generate session token
            var sessionToken = _session.GenCookie(login.Credential);

            // Set session token cookie
            Response.Cookies.Add(sessionToken);

            // Check if the cookie exists after setting it
            var cookie = Request.Cookies["X-KEY"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Log the token from the cookie
            var token = cookie.Value;

            // Fetch user details using the token
            var userMinimal = _session.GetUserByCookie(token);
            if (userMinimal == null)
            {
                System.Diagnostics.Debug.WriteLine("No user found with the given token.");
                return RedirectToAction("Login", "Login");
            }

            // Store username in session
            Session["Username"] = userMinimal.Username;

            // Redirect based on user role
            if (userLogin.Level == LevelAcces.Admin)
            {
                return RedirectToAction("Admin", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(URegister register)
        {
            if (ModelState.IsValid)
            {
                URegister data = new URegister
                {
                    Email = register.Email,
                    Username = register.Email,
                    Password = register.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now,
                    Role = LevelAcces.User,
                };

                var userRegister = _session.UserRegister(data);
                if (userRegister.Status)
                {
                    ViewBag.Status = "Succes";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Status = "Insucces";
                    return View("Register");
                }

            }


            return View("Register");
        }

    }

}
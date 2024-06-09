using System;
using System.Web;
using System.Web.Mvc;
using Time_Zone.BussinessLogic;
using Time_Zone.Domain.Enums;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Extension;
using System.Web.Security;
using Time_Zone.BussinesLogic;
using Time_Zone.Models;

namespace Time_Zone.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new Time_Zone.BusinessLogic.BusinessLogicService();
            _session = bl.GetSessionBL();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("About", "Home");
            }

            UserLogin data = new UserLogin
            {
                Credential = login.Credential,
                Password = login.Password,
                LoginIp = Request.UserHostAddress,
                LoginDateTime = DateTime.Now,
            };

            var userLogin = _session.UserLogin(data);
            if (!userLogin.Status)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return RedirectToAction("About", "Home");
            }

            var sessionToken = _session.GenCookie(login.Credential);
            Response.Cookies.Add(sessionToken);

            var token = sessionToken.Value;
            var userMinimal = _session.GetUserByCookie(token);
            if (userMinimal == null)
            {
                System.Diagnostics.Debug.WriteLine("No user found with the given token.");
                return RedirectToAction("Login", "Login");
            }

            Session["Username"] = userMinimal.Username;
            HttpContext.SetMySessionObject(userMinimal);

            // Redirecționează utilizatorul la pagina de administrare dacă este admin
            if (userLogin.Level == LevelAcces.Admin)
            {
                return RedirectToAction("Admin", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult GetUserStatus()
        {
            if (Session["Username"] != null)
            {
                var username = Session["Username"].ToString();
                return PartialView("_UserStatus", new UserStatusViewModel
                {
                    Username = username,
                    IsAuthenticated = true
                });
            }
            else
            {
                return PartialView("_UserStatus", new UserStatusViewModel
                {
                    IsAuthenticated = false
                });
            }
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
                    ViewBag.Status = "Success";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Status = "Failure";
                    return View("Register");
                }
            }

            return View("Register");
        }
    }
}

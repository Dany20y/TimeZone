using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Time_Zone.BussinesLogic;
using Time_Zone.BussinessLogic;
using Time_Zone.Domain.Entities.Product;

namespace Time_Zone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;

        public HomeController()
        {
            var bl = new Time_Zone.BusinessLogic.BusinessLogicService();
            _session = bl.GetSessionBL();
        }

        public ActionResult ProductDetails(int productId)
        {
            var product = _session.GetProductById(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);  // Pass product to the view
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Shop()
        {
            return RedirectToAction("Shop", "Products");
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogDetails()
        {
            return View();
        }
        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult UProfile()
        {
            var token = Request.Cookies["X-KEY"]?.Value;

            if (token != null)
            {
                var UserSession = _session.GetSessionByCookie(token);
                var session = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now)
                {
                    ViewBag.Username = session.Username;
                    return View(new PasswordChangeModel());
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Session Expired");
                    TempData["Error"] = "Session expired. Please log in again.";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No session found");
                TempData["Error"] = "No active session found. Please log in.";
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PasswordChangeModel model)
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Invalid model");
                return View("UProfile", model);
            }

            string user = null;

            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var userMinimal = _session.GetUserByCookie(token);
                user = userMinimal?.Username;
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            if (user == null)
            {
                System.Diagnostics.Debug.WriteLine("User not recognized");
                ModelState.AddModelError("", "User not recognized.");
                return View("UProfile", model);
            }

            bool result = _session.ChangeUserPassword(user, model.OldPassword, model.NewPassword);
            if (result)
            {
                System.Diagnostics.Debug.WriteLine("Password updated");
                TempData["Message"] = "Password successfully updated.";
                return RedirectToAction("UProfile");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Old password is incorrect or update failed");
                ModelState.AddModelError("", "The old password is incorrect or update failed.");
                return View("UProfile", model);
            }
        }
    }
}

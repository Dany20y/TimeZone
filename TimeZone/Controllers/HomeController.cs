using System;
using System.Collections.Generic;
/*using System.Linq;*/
using System.Web;
using System.Web.Mvc;

namespace Time_Zone.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            string role = SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View();
            }
            return View();
        }
        public ActionResult Shop()
        {
                return View(); 
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult ProductDetails()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogDetails()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Confirmation()
        {
            return View();
        }
        public ActionResult ProductCheckout() 
        { 
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}
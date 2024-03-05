using System;
using System.Collections.Generic;
/*using System.Linq;*/
using System.Web;
using System.Web.Mvc;

namespace TimeZone.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
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
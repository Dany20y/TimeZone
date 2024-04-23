using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Time_Zone.Controllers.Attributes;
using AutoMapper;
using Time_Zone.Models;

namespace Time_Zone.Controllers
{
    public class AdminController : BaseController
    {
        [AdminMod]
        public ActionResult Admin()
        {
            return View();
        }

        [AdminMod]
        public ActionResult Index()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")

                return RedirectToAction("", "Login");
            }
            return View();


        }

    }
}
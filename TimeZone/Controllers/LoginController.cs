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


        // GET: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userLogin login)
        {   
            if(ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Username,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDataTime = DateTime.Now
                };

                var UserLogin = _session.UserLogin(data);
                if (UserLogin.Status) 
                {
                    LevelStatus status = _session.CheckLevel(UserLogin.SessionKey);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Nume de utilizator sau parola incorecta. Va rugam sa incercati din nou!", UserLogin.StatusMessage);
                    return View(login);
                }
            }
            return View(login);
        }
    }
}
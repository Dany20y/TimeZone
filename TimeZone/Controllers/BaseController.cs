using System;
using System.Linq;
using System.Web.Mvc;
using Time_Zone.BusinessLogic;
using Time_Zone.BussinesLogic; // Importă namespace-ul corect care conține ISession
using Time_Zone.Domain.Enums;
using Time_Zone.Extension;

namespace Time_Zone.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISession _session;

        public BaseController()
        {
            var bl = new BusinessLogicService(); // Folosește noul nume al clasei
            _session = bl.GetSessionBL();
        }


        // Verifică starea sesiunii curente
        public string SessionStatus()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null)
                {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                    System.Web.HttpContext.Current.Session["Username"] = profile.Username;

                    if (profile.Level == LevelAcces.Admin)
                        return "Admin";

                    return "User";
                }
                else
                {
                    ClearSessionAndCookie();
                    return "None";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                return "None";
            }
        }

        // Curăță sesiunea și cookie-urile
        private void ClearSessionAndCookie()
        {
            System.Web.HttpContext.Current.Session.Clear();
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }
            System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
        }
    }
}

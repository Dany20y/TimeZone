﻿using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Time_Zone.BusinessLogic;
using Time_Zone.Domain.Enums;
using Time_Zone.Web.Extension;

namespace TimeZone.Controllers.Atrributes
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;

        public AdminModAttribute()
        {
            var businessLogic = new BusinessLogic.BussinesLogic();
            _session = businessLogic.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie == null)
            {
                RedirectToLogin(filterContext);
                return;
            }

            var profile = _session.GetUserByCookie(apiCookie.Value);
            if (profile == null)
            {
                RedirectToLogin(filterContext);
                return;
            }

            if (profile.Level == LevelAcces.Admin)
            {
                HttpContext.Current.SetMySessionObject(profile);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Home", action = "ErrorAccessDenied" }));

            }
        }

        private void RedirectToLogin(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new { controller = "Login", action = "Login" }));
        }
    }
}
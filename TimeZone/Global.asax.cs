using Time_Zone.Models;
using AutoMapper;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Time_Zone;
using Time_Zone.Domain.Entities.User;
using TimeZone.Models;


namespace App
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg => {
                cfg.CreateMap<UDbTable, UserMinimal>();
                cfg.CreateMap<userLogin, ULoginData>();
                cfg.CreateMap<userRegister, URegisterData>();
            });
        }
    }
}
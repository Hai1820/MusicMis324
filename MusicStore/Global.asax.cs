using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Roles.CreateRole("Administrator");
            //Roles.CreateRole("User");
            //Code First 数据库初始化

            try
            {
                Roles.CreateRole("Administrator");
                Roles.CreateRole("User");
                Membership.CreateUser("admin", "!123456", "admin@gmail.com");
                Roles.AddUserToRole("admin", "Administrator");
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
           
            System.Data.Entity.Database.SetInitializer(new MusicStore.EntityContext.SampleData());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

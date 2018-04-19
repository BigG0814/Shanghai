using Microsoft.AspNet.Identity;
using Shanghai.Security.BLL;
using Shanghai.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Shanghai.WebApp
{
    public class Global : HttpApplication
    {
        public static int userCartId;

        public void Session_Start(object sender, EventArgs e)
        {
            var controller = new CartController();
            controller.ShoppingCartCleanup();
            userCartId = controller.StartNewCart();
        }
        public void Session_OnEnd(object sender, EventArgs e)
        {
            var controller = new CartController();

            if(userCartId != 0)
            {
                controller.DeleteOpenCart(userCartId);
            }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Setup default security roles
            var roleManager = new RoleManager();
            roleManager.AddStartupRoles();

            // Add POSSYSTEM and ManagerOne accounts
            var userManager = new UserManager();
            userManager.AddPOSSYSTEM();
            userManager.AddManagerOne();

        }
    }
}
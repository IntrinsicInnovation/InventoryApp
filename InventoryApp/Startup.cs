using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Threading;
using Microsoft.AspNet.SignalR;
using InventoryApp.Controllers;

[assembly: OwinStartup(typeof(InventoryApp.Startup))]

namespace InventoryApp
{
    public class Startup
    {
        private static Thread notificationThread = new Thread(() =>
        {

            var controller = new InventoryController();
            var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            while (true)
            {

                var expiredProducts = controller.GetExpiredProducts();
                if (expiredProducts != null )
                {
                    foreach (var expired in expiredProducts)
                    {
                        notificationHub.Clients.All.update(expired.Name + " has expired");
                    }
                }
               
                Thread.Sleep(5000);
            }
        });

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            notificationThread.Start();
        }
        

    }
}

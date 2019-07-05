using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerService.Services
{
    public class HostService
    {

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IDisposable _host;

        public void Start()
        {
            try
            {
                _logger.Info("WebHost with HangFire is starting");

                var port = int.Parse(ConfigurationManager.AppSettings["port"]);

                var options = new StartOptions { Port = port };
                _host = WebApp.Start<Startup>(options);

                _logger.Info("WebHost with HangFire has started");
                _logger.Info($"Dashboard is available at http://localhost:{port}/hangfire");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "error during starting web host");
            }

        }

        public void Stop()
        {
            _host?.Dispose();
        }
    }



    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Hangfire configuration
            string connString = ConfigurationManager.ConnectionStrings["hangfireDB"].ConnectionString;
            GlobalConfiguration.Configuration.UseSqlServerStorage(connString);
            appBuilder.UseHangfireDashboard();
            appBuilder.UseHangfireServer();

            // WebAPI configuration
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            
            // attribute based routing
            config.MapHttpAttributeRoutes();
            
            // convention based routing config
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            appBuilder.UseWebApi(config);
        }
    }
}

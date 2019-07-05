using SchedulerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SchedulerService
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.UseNLog();

                x.Service<HostService>(s =>
                {
                    s.ConstructUsing(name => new HostService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                //x.RunAs("user", "password");

                x.SetDisplayName("Scheduler Service");
                x.SetServiceName("Scheduler Service");
                x.SetDescription("Scheduler Service");

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(1);
                });
            });

            // additional configuration:
            // http://docs.topshelf-project.com/en/latest/configuration/config_api.html


            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}

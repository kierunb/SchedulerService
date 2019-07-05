using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Jobs
{
    public class SimpleJob
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Execute()
        {
            _logger.Info("SimpleJob executed");
        }
    }
}

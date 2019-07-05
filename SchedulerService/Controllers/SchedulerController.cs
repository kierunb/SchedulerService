using Hangfire;
using NLog;
using SchedulerService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerService.Controllers
{
    public class SchedulerController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        // TODO: Change GET to POST

        [Route("api/scheduler/one-time")]
        [HttpGet]
        public IHttpActionResult ScheduleOneTimeJob()
        {

            var simpleJob = new SimpleJob();

            var jobId = BackgroundJob.Enqueue(() => simpleJob.Execute());

            return Ok($"fire and forget job {jobId} fired");
        }


        [Route("api/scheduler/recurring")]
        [HttpGet]
        public IHttpActionResult ScheduleJob()
        {

            var simpleJob = new SimpleJob();

            RecurringJob.AddOrUpdate(() => simpleJob.Execute(), Cron.Minutely);

            return Ok($"recurring job scheduled");
        }
    }
}

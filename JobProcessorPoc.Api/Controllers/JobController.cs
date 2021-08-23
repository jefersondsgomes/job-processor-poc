using JobProcessorPoc.Api.Request;
using JobProcessorPoc.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobProcessorPoc.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPods()
        {
            var result = await _jobService.GetPods();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] JobRequest job)
        {
            var result = await _jobService.CreateJob(job.NumberOfJobs);
            return new OkObjectResult(result);
        }
    }
}
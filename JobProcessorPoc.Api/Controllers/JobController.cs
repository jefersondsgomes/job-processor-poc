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

        public JobController()
        {
            _jobService = new JobService();
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            await _jobService.CreateJobsAsync(10);
            return new NoContentResult();
        }
    }
}
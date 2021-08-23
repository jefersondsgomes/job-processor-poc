using System.Threading.Tasks;

namespace JobProcessorPoc.Api.Services
{
    public interface IJobService
    {
        Task CreateJobsAsync(int numberOfJobs);
    }
}

using k8s.Models;
using System.Threading.Tasks;

namespace JobProcessorPoc.Api.Services
{
    public interface IJobService
    {
        Task<V1PodList> GetPods();
        Task<V1Job> CreateJob(int numberOfJobs);
    }
}
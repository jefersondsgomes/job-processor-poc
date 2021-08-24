using k8s;
using k8s.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobProcessorPoc.Api.Services
{
    public class JobService : IJobService
    {
        private readonly IKubernetes _kubernetes;

        public JobService(IKubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        public async Task<V1Job> CreateJob(int numberOfJobs)
        {
            var job = new V1Job()
            {
                ApiVersion = "batch/v1",
                Kind = "Job",
                Metadata = new V1ObjectMeta() { GenerateName = "job-processor-poc-" },
                Spec = new V1JobSpec()
                {
                    TtlSecondsAfterFinished = 5,
                    Template = new V1PodTemplateSpec()
                    {
                        Spec = new V1PodSpec()
                        {
                            RestartPolicy = "Never",
                            Containers = new List<V1Container>()
                            {
                                new V1Container()
                                {
                                    Name = "job-processor-poc",
                                    Image = "jefersondsgomes/job-processor-poc:latest",
                                    Args = new List<string>() { numberOfJobs.ToString() }
                                }
                            }
                        }
                    }
                }
            };

            return await _kubernetes.CreateNamespacedJobAsync(job, "default");
        }

        public async Task<V1PodList> GetPods()
        {
            return await _kubernetes.ListNamespacedPodAsync("default");
        }
    }
}
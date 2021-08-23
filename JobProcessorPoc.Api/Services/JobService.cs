using k8s;
using System;
using System.Threading.Tasks;

namespace JobProcessorPoc.Api.Services
{
    public class JobService : IJobService
    {
        public JobService()
        {

        }

        public async Task CreateJobsAsync(int numberOfJobs)
        {
            Environment.SetEnvironmentVariable("KUBERNETES_SERVICE_HOST", "localhost");

            var config = KubernetesClientConfiguration.BuildDefaultConfig();
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var podList = await client.ListNamespacedPodAsync("default");
            foreach (var pod in podList.Items)
            {
                Console.Write(pod.Metadata.Name);
            }
        }
    }
}
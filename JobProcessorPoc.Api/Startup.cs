using JobProcessorPoc.Api.Services;
using k8s;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace JobProcessorPoc.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private static string K8S_CONFIG;
        private static string K8S_CONTEXT;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            K8S_CONFIG = Environment.GetEnvironmentVariable("K8S_CONFIG");
            K8S_CONTEXT = Environment.GetEnvironmentVariable("K8S_CONTEXT");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IKubernetes>(new Kubernetes(KubernetesClientConfiguration.BuildConfigFromConfigFile(K8S_CONFIG, K8S_CONTEXT)));
            services.AddSingleton<IJobService, JobService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

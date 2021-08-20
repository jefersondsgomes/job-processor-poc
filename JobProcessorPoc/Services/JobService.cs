using JobProcessorPoc.Interfaces;
using JobProcessorPoc.Models;
using System;
using System.Threading;

namespace JobProcessorPoc.Services
{
    class JobService : IJobService
    {
        public Job Create(string name)
        {
            Logger.Log($"Creating: {name}");
            var job = new Job
            {
                Name = name,
                Start = DateTime.Now,
                Status = JobStatus.Created
            };

            WaitExecute();
            Logger.Log(job.ToString());
            return job;
        }

        public Job Processing(Job job)
        {
            Logger.Log($"Processing: {job.Name}");
            job.Status = JobStatus.Processing;

            WaitExecute();
            Logger.Log(job.ToString());
            return job;
        }

        public Job Finish(Job job)
        {
            Logger.Log($"Finishing: {job.Name}");
            job.End = DateTime.Now;
            job.Status = JobStatus.Finished;

            WaitExecute();
            Logger.Log(job.ToString());            
            return job;
        }

        private static void WaitExecute()
        {
            int secconds = new Random().Next(0, 8);
            Thread.Sleep(TimeSpan.FromSeconds(secconds));
        }
    }
}
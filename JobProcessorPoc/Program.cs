using JobProcessorPoc.Interfaces;
using JobProcessorPoc.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JobProcessorPoc
{
    class Program
    {
        private static readonly IJobService _jobService;

        static Program()
        {
            _jobService = new JobService();
        }

        static void Main(string[] args)
        {
            Init();
            var sw = new Stopwatch();
            sw.Start();

            if (!int.TryParse(args[0], out var numberOfJobs) || numberOfJobs == 0)
            {
                Logger.Log("Invalid entry parameter.");
                return;
            }

            var jobs = GenerateJobNames(numberOfJobs);
            HandleJobs(jobs);

            sw.Stop();
            Logger.Log($"Duration: {sw.Elapsed.ToString("hh':'mm':'ss")}");
        }

        static void Init()
        {
            Logger.Log("JOB PROCESSOR POC");
            Logger.Log($"Starting: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            Console.WriteLine(Environment.NewLine);
        }

        static void HandleJobs(ICollection<string> jobNames)
        {
            foreach (var jobName in jobNames)
            {
                var sw = new Stopwatch();
                sw.Start();

                var job = _jobService.Create(jobName);
                job = _jobService.Processing(job);
                job = _jobService.Finish(job);

                sw.Stop();
                Logger.Log($"Job Duration: {sw.Elapsed.ToString("hh':'mm':'ss")}");
                Console.WriteLine(Environment.NewLine);
            }
        }

        static ICollection<string> GenerateJobNames(int numberOfJobs)
        {
            var names = new List<string>();
            for (int i = 1; i <= numberOfJobs; i++)
            {
                var name = $"Job - #{i.ToString().PadLeft(5, '0')}";
                names.Add(name);
            }

            return names;
        }
    }
}
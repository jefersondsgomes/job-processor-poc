using JobProcessorPoc.Interfaces;
using JobProcessorPoc.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            Logger.Log("JOB PROCESSOR POC");
            Logger.Log($"Starting: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            Console.WriteLine(Environment.NewLine);

            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var numberOfJobs = GetTotals(args);
                var jobs = GenerateJobNames(numberOfJobs);
                HandleJobs(jobs);
            }
            catch (Exception e)
            {
                Logger.Log($"Error: {e.Message}");
                Console.WriteLine(Environment.NewLine);
            }

            sw.Stop();
            Logger.Log($"Duration: {sw.Elapsed.ToString("hh':'mm':'ss")}");
        }

        static int GetTotals(string[] args)
        {
            if (!args.Any())
                throw new Exception($"The application not received entry arguments.");

            if (!int.TryParse(args[0], out var numberOfJobs))
                throw new Exception($"Invalid entry argument: '{args[0]}'.");

            if (numberOfJobs == 0)
                throw new Exception("Entry argument must be greater than zero.");

            return numberOfJobs;
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
    }
}
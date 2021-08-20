using System;

namespace JobProcessorPoc.Models
{
    public class Job
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public JobStatus Status { get; set; }

        public Job()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"JobId: {Id} - Name: {Name} - Status: {Status}";
        }
    }
}
using JobProcessorPoc.Models;

namespace JobProcessorPoc.Interfaces
{
    public interface IJobService
    {
        Job Create(string name);
        Job Processing(Job job);
        Job Finish(Job job);
    }
}
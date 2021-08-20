using System.ComponentModel;

namespace JobProcessorPoc.Models
{
    public enum JobStatus
    {
        [Description("Created")]
        Created = 0,
        [Description("Processing")]
        Processing = 1,
        [Description("Finished")]
        Finished = 2,
        [Description("Error")]
        Error = 3
    }
}
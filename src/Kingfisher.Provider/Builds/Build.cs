using System;
using Newtonsoft.Json;

namespace Kingfisher.Provider.Builds
{
    public class Build
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public DateTimeOffset QueueTime { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? FinishTime { get; set; }
        public string Url { get; set; }
        public Definition Definition { get; set; }
        public Project Project { get; set; }
        public string Uri { get; set; }
        public string SourceVersion { get; set; }
        public Controller Controller { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
        public User RequestedFor { get; set; }
        public User RequestedBy { get; set; }
        public DateTimeOffset LastChangedDate { get; set; }
        public User LastChangedBy { get; set; }
        public string Parameters { get; set; }
        public Logs Logs { get; set; }
        public Repository Repository { get; set; }
        public bool KeepForever { get; set; }
    }
}

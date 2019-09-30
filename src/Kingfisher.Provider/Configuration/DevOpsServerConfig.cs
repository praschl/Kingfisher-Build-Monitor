using System;

namespace Kingfisher.Provider.Configuration
{
    public class DevOpsServerConfig
    {
        public string Url { get; set; }
        public TimeSpan BuildRefreshTime { get; set; } = TimeSpan.FromSeconds(15);

        public TimeSpan AgeOfBuilds { get; set; } = TimeSpan.FromDays(3);
    }
}
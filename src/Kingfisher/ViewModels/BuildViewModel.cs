using System;
using System.ComponentModel;

namespace Kingfisher.ViewModels
{
    public class BuildViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string Project { get; set; }

        public string Definition { get; set; }

        public BuildState Status { get; set; }
        
        public string RequestedFor { get; set; }

        public string RequestedBy { get; set; }

        public DateTime QueuedDateTime { get; set; }

        public DateTime? StartedDateTime { get; set; }

        public DateTime? FinishedDateTime { get; set; }

        public string WebUrl { get; set; }

        public DateTime ChangedAt => FinishedDateTime ?? StartedDateTime ?? QueuedDateTime;

        public string RequestedByShort { get; set; }

        public string RequestedForShort { get; set; }
        
        public TimeSpan Duration => (FinishedDateTime ?? DateTime.Now) - QueuedDateTime;
        public static BuildViewModel DesignTimeModel = new BuildViewModel
        {
            Id = 1,
            Project = "My projects long long text",
            Definition = "My build 1 long long text",
            RequestedBy = "TFS",
            RequestedFor = "Me long long name, very long!",
            FinishedDateTime = DateTime.Now,
            StartedDateTime = DateTime.Now.AddMinutes(-2),
            QueuedDateTime = DateTime.Now.AddMinutes(-3),
            Status = BuildState.failed
        };

        public void RefreshTimeProperties()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ChangedAt)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Duration)));
        }
    }
}

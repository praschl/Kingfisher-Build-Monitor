using System;

namespace Kingfisher.Provider.Builds
{
    public class Controller
    {
        public string Uri { get; set; }
        public string Status { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

}

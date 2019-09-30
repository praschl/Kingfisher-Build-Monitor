using System;

namespace Kingfisher.Provider.Builds
{
    public class Definition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }

}

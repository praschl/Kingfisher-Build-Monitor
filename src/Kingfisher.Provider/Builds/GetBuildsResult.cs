using System.Collections.Generic;

namespace Kingfisher.Provider.Builds
{
    public class GetBuildsResult
    {
        public int Count { get; set; }
        public IList<Build> Value { get; set; }
    }
}

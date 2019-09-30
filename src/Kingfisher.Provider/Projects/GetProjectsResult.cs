using System.Collections.Generic;

namespace Kingfisher.Provider.Projects
{
    public class GetProjectsResult
    {
        public int Count { get; set; }
        public IList<Project> Value { get; set; }
    }
}

using System.Collections.Generic;

namespace Kingfisher.Provider.BuildArtifacts
{
    public class GetBuildArtifactsResult
    {
        public int Count { get; set; }
        public IReadOnlyList<Value> Value { get; set; }
    }
}

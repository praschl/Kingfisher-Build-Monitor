using System.Collections.Generic;
using Kingfisher.Provider.Builds;

namespace Kingfisher.ViewModels.Mappers
{
    public interface IBuildsMapper
    {
        void Map(IReadOnlyList<Build> builds, ICollection<BuildViewModel> buildsCollection);
    }
}
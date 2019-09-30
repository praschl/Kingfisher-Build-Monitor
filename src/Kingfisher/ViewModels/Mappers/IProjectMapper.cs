using System.Collections.Generic;
using Kingfisher.Provider.Projects;

namespace Kingfisher.ViewModels.Mappers
{
    public interface IProjectMapper
    {
        void Map(IReadOnlyCollection<Project> projects, ICollection<ProjectViewModel> projectsCollection);
    }
}
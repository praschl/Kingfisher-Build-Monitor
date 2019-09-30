using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingfisher.Provider.Projects;

namespace Kingfisher.ViewModels.Mappers
{
    public class ProjectMapper : IProjectMapper
    {
        public void Map(IReadOnlyCollection<Project> projects, ICollection<ProjectViewModel> projectsCollection)
        {
            foreach (var project in projects)
            {
                if (!projectsCollection.Any(p => p.Id == project.Id))
                {
                    var newProject = new ProjectViewModel { Id = project.Id, Name = project.Name };
                    projectsCollection.Add(newProject);
                }
            }
        }
    }
}

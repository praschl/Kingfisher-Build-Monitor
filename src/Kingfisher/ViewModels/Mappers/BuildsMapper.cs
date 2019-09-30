using System;
using System.Collections.Generic;
using System.Linq;
using Kingfisher.Provider.Builds;

namespace Kingfisher.ViewModels.Mappers
{
    public class BuildsMapper : IBuildsMapper
    {
        public void Map(IReadOnlyList<Build> builds, ICollection<BuildViewModel> buildsCollection)
        {
            foreach (var build in builds)
            {
                var existingBuildViewModel = buildsCollection.FirstOrDefault(b => b.Id == build.Id);
                var addToList = existingBuildViewModel == null;

                var buildViewModel = ConvertToViewModel(build, existingBuildViewModel);

                if (addToList)
                    buildsCollection.Add(buildViewModel);
            }
        }

        private BuildViewModel ConvertToViewModel(Build build, BuildViewModel buildVm)
        {
            if (buildVm == null)
                buildVm = new BuildViewModel();

            // if the view model has already been set to finished, do not change any properties.
            if (buildVm.FinishedDateTime.HasValue)
                return buildVm;

            buildVm.Id = build.Id;
            buildVm.Project = build.Project.Name;
            buildVm.Definition = build.Definition.Name;
            buildVm.RequestedBy = build.RequestedBy.DisplayName;
            buildVm.RequestedFor = build.RequestedFor.DisplayName;
            buildVm.RequestedByShort = build.RequestedBy.UniqueName;
            buildVm.RequestedForShort = build.RequestedFor.UniqueName;
            buildVm.QueuedDateTime = build.QueueTime.ToLocalTime().DateTime;
            buildVm.StartedDateTime = build.StartTime?.ToLocalTime().DateTime;
            buildVm.FinishedDateTime = build.FinishTime?.ToLocalTime().DateTime;
            buildVm.Status = GetVmBuildStatus(build.Status, build.Result);
            buildVm.WebUrl = build.Links?.Web?.Href;

            return buildVm;
        }

        private BuildState GetVmBuildStatus(string status, string result)
        {
            if (Enum.TryParse(result, out BuildState rResult))
                return rResult;

            if (Enum.TryParse(status, out BuildState rStatus))
                return rStatus;

            return BuildState.unknown;
        }
    }
}
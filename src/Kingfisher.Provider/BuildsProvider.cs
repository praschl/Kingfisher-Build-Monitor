using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kingfisher.Provider.Builds;
using Project = Kingfisher.Provider.Projects.Project;

namespace Kingfisher.Provider
{
    public class BuildsProvider : IBuildsProvider
    {
        private readonly Func<IDevOpsApi> _devOpsApi;

        public BuildsProvider(Func<IDevOpsApi> devOpsApi)
        {
            _devOpsApi = devOpsApi;
        }

        public async Task<IReadOnlyList<Project>> GetProjectsAsync()
        {
            var result = await _devOpsApi().GetProjects().ConfigureAwait(false);
            return result.Value.ToArray();
        }

        public async Task<IReadOnlyList<Build>> GetFinishedBuildsAsync(string[] projects, DateTimeOffset since)
        {
            var tasks = projects.Select(project => _devOpsApi().GetFinishedBuilds(project, since)).ToArray();

            var result = (await Task.WhenAll(tasks).ConfigureAwait(false))
                .SelectMany(b => b.Value)
                .ToArray();

            return result;
        }

        public async Task<IReadOnlyList<Build>> GetOpenBuildsAsync(string[] projects)
        {
            var tasks = new List<Task<GetBuildsResult>>();
            foreach (var project in projects)
            {
                tasks.Add(_devOpsApi().GetOpenBuilds(project));
            }

            var result = (await Task.WhenAll(tasks).ConfigureAwait(false))
                .SelectMany(b => b.Value)
                .ToArray();

            return result;
        }

        public async Task<string> GetDropFolderPathAsync(string project, int buildId)
        {
            var result = await _devOpsApi().GetBuildArtifacts(project, buildId);

            var resource = result.Value?.FirstOrDefault(v => v.Name == "drop")?.Resource;

            if (resource == null)
                return string.Empty;

            if (resource.Type != "FilePath")
                return string.Empty;

            return resource.Data;
        }
    }
}
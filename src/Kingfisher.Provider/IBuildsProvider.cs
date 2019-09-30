using Kingfisher.Provider.Builds;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kingfisher.Provider
{
    public interface IBuildsProvider
    {
        Task<IReadOnlyList<Projects.Project>> GetProjectsAsync();

        Task<IReadOnlyList<Build>> GetFinishedBuildsAsync(string[] projects, DateTimeOffset since);

        Task<IReadOnlyList<Build>> GetOpenBuildsAsync(string[] projects);

        Task<string> GetDropFolderPathAsync(string project, int buildId);
    }
}
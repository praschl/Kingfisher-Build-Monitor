using Kingfisher.Provider.Builds;
using Kingfisher.Provider.Projects;
using RestEase;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kingfisher.Provider.BuildArtifacts;

namespace Kingfisher.Provider
{
    public interface IDevOpsApi
    {
        /// <summary>
        /// This request is used to test if the server can be reached.
        /// </summary>
        [Get("_apis/projects?$top=1")]
        [AllowAnyStatusCode]
        Task<HttpResponseMessage> IsOnline(CancellationToken cancellationToken);
        
        [Get("_apis/projects")]
        Task<GetProjectsResult> GetProjects();

        [Get("{projectName}/_apis/build/builds?api-version=2.3&minFinishTime={minFinishTime}")]
        Task<GetBuildsResult> GetFinishedBuilds([Path] string projectName, [Path(Format = "u")] DateTimeOffset minFinishTime);

        [Get("{projectName}/_apis/build/builds?api-version=2.3&statusFilter=inProgress,cancelling,postponed,notStarted")]
        Task<GetBuildsResult> GetOpenBuilds([Path] string projectName);

        [Get("{projectName}/_apis/build/builds/{buildId}/artifacts?api-version=2.3")]
        Task<GetBuildArtifactsResult> GetBuildArtifacts([Path] string projectName, [Path] int buildId);
    }

    public interface IDisposableDevOpsApi : IDevOpsApi, IDisposable
    {
    }
}

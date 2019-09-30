using System.Net.Http;

namespace Kingfisher.Provider
{
    public interface IDevOpsApiProvider
    {
        /// <summary>
        ///     Gets a <see cref="IDevOpsApi"/> for an uri. Instances are reused and shared between threads.
        /// </summary>
        IDevOpsApi GetDevOpsApi(string uri);

        /// <summary>
        ///     Removes a <see cref="IDevOpsApi"/> by its uri and disposes the <see cref="HttpClient"/>
        /// </summary>
        void Remove(string uri);
    }
}
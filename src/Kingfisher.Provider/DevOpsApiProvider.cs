using System;
using System.Collections.Concurrent;
using System.Net.Http;
using RestEase;

namespace Kingfisher.Provider
{
    public class DevOpsApiProvider : IDevOpsApiProvider
    {
        private readonly ConcurrentDictionary<string, IDevOpsApi> _cachedApis = new ConcurrentDictionary<string, IDevOpsApi>();

        public IDevOpsApi GetDevOpsApi(string uri)
        {
            return _cachedApis.GetOrAdd(uri, CreateDevOpsApi);
        }

        public void Remove(string uri)
        {
            // when no uri was configured before
            if (uri == null)
                return;

            if (_cachedApis.TryRemove(uri, out var api) 
                && api is IDisposable disposableApi)
            {
                disposableApi.Dispose();
            }
        }

        private IDevOpsApi CreateDevOpsApi(string uri)
        {
            var handler = new HttpClientHandler {UseDefaultCredentials = true};

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(uri)
            };

            var requester = new DevOpsApiRequester(httpClient);

            var restClient = RestClient.For<IDisposableDevOpsApi>(requester);

            return restClient;
        }
    }
}
using System;
using System.Net.Http;
using RestEase;
using RestEase.Implementation;

namespace Kingfisher.Provider
{
    public class DevOpsApiRequester : Requester
    {
        public DevOpsApiRequester(HttpClient httpClient) : base(httpClient)
        {
        }

        protected override Uri ConstructUri(string basePath, string path, IRequestInfo requestInfo)
        {
            var constructUri = base.ConstructUri(basePath, path, requestInfo);

            //Debug.WriteLine($"URL: {constructUri}");

            return constructUri;
        }

        protected override T Deserialize<T>(string content, HttpResponseMessage response, IRequestInfo requestInfo)
        {
            //Debug.WriteLine(content);

            return base.Deserialize<T>(content, response, requestInfo);
        }
    }
}
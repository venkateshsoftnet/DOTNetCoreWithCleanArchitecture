using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TFLAssessment.Application;
using TFLAssessment.Application.Exceptions;
using TFLAssessment.Application.Interfaces;
using TFLAssessment.Domain.Entities;

namespace TFLAssessment.Infrastructure.Shared.HttpClients
{
    public class RoadClient : IRoadClient
    {
        private readonly HttpClient httpClient;
        private readonly ApiSettings settings;

        public RoadClient(HttpClient httpClient, IOptions<ApiSettings> settings)
        {
            this.httpClient = httpClient;
            this.settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<List<Road>> GetRoadStatusAsync(RoadStatusQuery roadStatusQuery, CancellationToken cancellationToken)
        {
            if (roadStatusQuery.RoadIds == null || roadStatusQuery.RoadIds.Count <= 0) throw new ApiException("Invalid input");

            const string route = "Road/";
            string requestUri = settings.BaseURL + route + string.Join(",", roadStatusQuery.RoadIds);
            string url = requestUri + "?app_id=" + settings.AppId + "&app_key=" + settings.AppKey;
            var requestUrl = new Uri(url);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            var response = new HttpResponseMessage();

            try
            {
                response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ApiException("Unknown error", ex.Message);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<List<Road>>(contentStream, new JsonSerializerOptions(), cancellationToken);
            }
            else
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(contentStream, new JsonSerializerOptions(), cancellationToken);

                throw new ApiException(error.message);
            }
        }
    }
}

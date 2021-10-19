using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TFL.Road.Status.Application;
using TFL.Road.Status.Application.Exceptions;
using TFL.Road.Status.Application.Interfaces;
using TFL.Road.Status.Domain.Entities;

namespace TFL.Road.Status.Infrastructure.Shared.HttpClients
{
    public class RoadClient : IRoadClient
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;

         public RoadClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<List<Roads>> GetRoadStatusAsync(RoadStatusQuery roadStatusQuery, CancellationToken cancellationToken)
        {
            var url = configuration["ApiSettings:TflApi"];
            var appId = configuration["ApiSettings:AppId"];
            var appKey = configuration["ApiSettings:AppKey"];

            //string urlSafeText = Uri.EscapeDataString(roadName.Replace("\n", " ")+"?app_id="+_appId+"&app_key="+_appKey);
            string urlSafeText = roadStatusQuery.RoadId + "?app_id=" + appId + "&app_key=" + appKey;
            var requestUrl = new Uri(url + urlSafeText);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            var response = new HttpResponseMessage();

            try
            {
                response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<List<Roads>>(contentStream, new JsonSerializerOptions(), cancellationToken);
                    }

                case HttpStatusCode.NotFound:
                    throw new NoResultsFoundException();

                default:
                    throw new ApiException($"Status code returned: {response.StatusCode}");
            }
        }
    }

}

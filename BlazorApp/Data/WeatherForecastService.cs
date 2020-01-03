using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlazorApp.Data
{

    public class WeatherForecastService : IWeatherForcastService
    {
        private readonly ApiClient _httpClient;
       // private readonly ITokenAcquisition _tokenAcquisition; /* this cannot be constructed outside of controllers */
        //private readonly string _Scope;
        //private readonly string _BaseAddress = "https://localhost:44381";

        public WeatherForecastService(ApiClient httpClient)
        {
            this._httpClient = httpClient;
            //this._Scope = configuration["WeatherForcast:Scope"];
            //this._BaseAddress = configuration["WeatherForcast:BaseAddress"];
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            //await PrepareAuthenticatedClient(); /* this is required for access token, but unfortunately we cannot use it in this way */
            var response = await _httpClient.GetAsync($"api/weatherforecast");
            /* the api call is of course unauthorized as no access token was given */
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                IEnumerable<WeatherForecast> weatherForecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(content);

                return weatherForecasts.ToArray();
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }
        /* The below method to get authenticated access token cannot be used because ITokenAcquisition instances cannot be constructed outside of controllers */
        //private async Task PrepareAuthenticatedClient()
        //{
        //    var accessToken = await this._tokenAcquisition.GetAccessTokenOnBehalfOfUserAsync(new[] { this._Scope });
        //    this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}

    }
}

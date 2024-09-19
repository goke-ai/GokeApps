using Goke.Web.Shared.Models;
using System.Net.Http.Json;

namespace Goke.Web.Client.Services
{
    public class WeatherForecastService
    {
        private readonly HttpClient http;

        public WeatherForecastService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<WeatherForecast[]?> GetWeatherForecast()
        {
            return await http.GetFromJsonAsync<WeatherForecast[]?>("api/WeatherForecast");
        }
    }
}

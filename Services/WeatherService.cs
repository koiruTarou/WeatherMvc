using System.Net.Http;
using System.Net.Http.Json;
using WeatherMvc.Models;

namespace WeatherMvc.Services
{
    public class WeatherService
    {
        private readonly HttpClient _http;

        public WeatherService(HttpClient http)
        {
            _http = http;
        }

        public async Task<WeatherInfo> GetTodayWeatherAsync(string jmaCode)
        {
            var url = $"https://wgnfmubeg8.execute-api.ap-northeast-1.amazonaws.com/Prod/weather/{jmaCode}";
            return await _http.GetFromJsonAsync<WeatherInfo>(url);
        }
    }

}

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherMvc.Models;
using WeatherMvc.Common;
using System.Diagnostics;

namespace WeatherMvc.Services
{
    public class WeatherService
    {
        private readonly HttpClient _http;

        public WeatherService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Dictionary<string, Dictionary<string, PrefEntry>>> BuildAreaTreeAsync()
        {
            var root = await LoadAreaJsonAsync();

            var tree = new Dictionary<string, Dictionary<string, PrefEntry>>();

            foreach (var center in root.centers)
            {
                var centerName = center.Value.name;
                tree[centerName] = new Dictionary<string, PrefEntry>();

                // ★ 県コードは offices の parent が center.Key のもの
                var prefCodes = root.offices
                    .Where(o => o.Value.parent == center.Key)
                    .Select(o => o.Key);

                foreach (var prefCode in prefCodes)
                {
                    var pref = root.offices[prefCode];
                    var prefName = pref.name;

                    // ★ class10 → class15 を正しく辿る
                    var cities = pref.children
                    .SelectMany(class10Code =>
                        root.class10s[class10Code].children.Select(class15Code => new CityEntry
                        {
                            name = root.class15s[class15Code].name,
                            code = class15Code,
                            parent = root.class15s[class15Code].parent
                        })
                    )
                    .ToList();

                    tree[centerName][prefName] = new PrefEntry
                    {
                        prefCode = prefCode,
                        cities = cities
                    };
                }
            }

            return tree;
        }


        public async Task<AreaRoot> LoadAreaJsonAsync()
        {
            using var client = new HttpClient();
            
            var json = await client.GetStringAsync(CommonConstants.AREA_INFO_URL);

            var areaRoot = JsonSerializer.Deserialize<AreaRoot>(json);
            return areaRoot;
        }

        public async Task<WeatherInfo> GetTodayWeatherAsync(string? prefCode, string? cityCode)
        {
            var url = $"{CommonConstants.WEATHER_API_URL}{prefCode}/{cityCode}";
            //var url = $"http://localhost:5000/weather/{prefCode}/{cityCode}";
            return await _http.GetFromJsonAsync<WeatherInfo>(url);
        }
    }


}

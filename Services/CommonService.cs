using System.Text.Json;

namespace WeatherMvc.Services;

public class CommonService
{
    private readonly IWebHostEnvironment _env;

    public CommonService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<Dictionary<string, Dictionary<string, string>>> GetCityCodeInfo()
    {
        var path = Path.Combine(_env.WebRootPath, "data", "citycodes.json");
        var json = await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json)
               ?? new Dictionary<string, Dictionary<string, string>>();
    }
}

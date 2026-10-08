using Microsoft.AspNetCore.Mvc;
using WeatherMvc.Services;   
using WeatherMvc.Models;

namespace WeatherMvc.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weather;

        public WeatherController(WeatherService weather)
        {
            _weather = weather;
        }

        // 都市コード入力画面
        [HttpGet]   

        public async Task<IActionResult> Index()
        {
            var vm = new LocationSelectViewModel
            {
                AreaTree = await _weather.BuildAreaTreeAsync()
            };

            return View(vm);
        }

        // 天気取得処理
        [HttpPost]
        public async Task<IActionResult> Result(LocationSelectViewModel model)
        {
            //選択した地域情報からAPIで取得できる親コードの値に変換する
            //例020011(東津軽のコード)→020010（津軽のコード）
            string forecastCode = model.CityCode.Substring(0, 5) + "0";
            //天気情報を取得
            model.weatherInfo = await _weather.GetTodayWeatherAsync(model.PrefCode,forecastCode);
            
            return View(model);
        }

        
    }
}

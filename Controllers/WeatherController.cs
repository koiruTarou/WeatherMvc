using Microsoft.AspNetCore.Mvc;
using WeatherMvc.Services;   
using WeatherMvc.Models;
using WeatherMvc.Constants;

namespace WeatherMvc.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weather;
                private readonly CommonService _common;


        public WeatherController(WeatherService weather,CommonService common)
        {
            _weather = weather;
            _common = common;
        }


        // 都市コード入力画面
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var groups = await _common.GetCityCodeInfo();
            return View(groups);
        }

        // 天気取得処理
        [HttpPost]

        public async Task<IActionResult> Result(string jmaCode)
        {
            // 都市コード一覧を取得
            var groups = await _common.GetCityCodeInfo();

            // 都市名を逆引きする
            string cityName = groups
                .SelectMany(g => g.Value)
                .FirstOrDefault(x => x.Value == jmaCode).Key;

            // 天気情報を取得
            var info = await _weather.GetTodayWeatherAsync(jmaCode);

            // 都市名をセット
            info.City = cityName;

            return View(info);
        }
    }
}

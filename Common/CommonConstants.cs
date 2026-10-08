

namespace WeatherMvc.Common;

public static class CommonConstants
{
    // 気象庁 API ベース URL（全体で使う）
    public const string WEATHER_API_URL = "https://wgnfmubeg8.execute-api.ap-northeast-1.amazonaws.com/Prod/weather/";
    // 地域情報取得URL
    public const string AREA_INFO_URL = "https://www.jma.go.jp/bosai/common/const/area.json";

}
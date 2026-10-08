namespace WeatherMvc.Models;
public class WeatherInfo
{
    public required string ResultCd{ get; set; } = "1";

    public required string ResultMsg{ get; set; } = "";
    
    public string? PrefCode { get; set; }
    public string? CityCode { get; set; }
    public string? Condition { get; set; } 
    public string? Comment { get; set; }
}


public class AreaRoot
{
    public Dictionary<string, Center> centers { get; set; }
    public Dictionary<string, Office> offices { get; set; }
    public Dictionary<string, Class15> class15s { get; set; }

     public Dictionary<string, Class15> class10s { get; set; }
}

public class Center
{
    public string name { get; set; }
    public List<string> children { get; set; } // 県コード
}

public class Office
{
    public string name { get; set; }
    public string parent { get; set; }         // 地方コード
    public List<string> children { get; set; } // 市区町村コード
}

public class Class15
{
    public string name { get; set; }
    public string parent { get; set; }         // 県コード

    public string[] children { get; set; }  
}

public class Class10
{
    public string name { get; set; }
     public string enname { get; set; }     
    public string parent { get; set; }         // 県コード
}

public class LocationSelectViewModel
{

    public string CenterCode { get; set; }  // 地方コード
    public string CenterName { get; set; }  // 地方名
    public string PrefCode { get; set; }    // 県コード
    public string PrefName { get; set; }    // 県名
    public string CityCode { get; set; }    // 市区町村コード
    public string CityName { get; set; }    // 市区町村名

    // 地方 → 県 → 市区町村の階層構造
    public Dictionary<string, Dictionary<string, PrefEntry>> AreaTree { get; set; }

    public WeatherInfo weatherInfo { get; set; } //取得した天気情報
}

public class CityEntry
{
    public string name { get; set; }
    public string code { get; set; }

    public string parent { get; set; }
}

public class PrefEntry
{
    public string prefCode { get; set; }
    public List<CityEntry> cities { get; set; }
}

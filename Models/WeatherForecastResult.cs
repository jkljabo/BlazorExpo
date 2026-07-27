namespace BlazorCodeChallenge.Models
{
    public class WeatherForecastResult
    {
        public List<ForecastPeriod> ForecastPeriods { get; set; } = [];

        public string RadarStation { get; set; } = "";
    }
}

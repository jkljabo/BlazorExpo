namespace BlazorCodeChallenge.Models
{
    public class ForecastResponse
    {
        public ForecastProperties properties { get; set; } = new();
    }

    public class ForecastProperties
    {
        public List<ForecastPeriod> periods { get; set; } = new();
    }

    public class ForecastPeriod
    {
        public int Number { get; set; }

        public string Name { get; set; } = "";

        public int Temperature { get; set; }

        public string TemperatureUnit { get; set; } = "";

        public string WindSpeed { get; set; } = "";

        public string WindDirection { get; set; } = "";

        public string ShortForecast { get; set; } = "";

        public string DetailedForecast { get; set; } = "";
    }
}

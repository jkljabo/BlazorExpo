namespace BlazorCodeChallenge.Models
{
    public class PointResponse
    {
        public PointProperties properties { get; set; } = new();
    }

    public class PointProperties
    {
        public string forecast { get; set; } = "";
        public string forecastHourly { get; set; } = "";
        public string radarStation { get; set; } = "";
    }
}

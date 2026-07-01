namespace BlazorCodeChallenge.Models
{
    public class CensusResponse
    {
        public CensusResult result { get; set; } = new();
    }

    public class CensusResult
    {
        public List<AddressMatch> addressMatches { get; set; } = new();
    }

    public class AddressMatch
    {
        public Coordinates coordinates { get; set; } = new();
    }

    public class Coordinates
    {
        public double x { get; set; }
        public double y { get; set; }
    }
}

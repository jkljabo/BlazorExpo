namespace BlazorCodeChallenge.Models
{

    public class MovieListResponse
    {
        //public Dates dates { get; set; }
        public int Page { get; set; }
        public List<Movie> Results { get; set; } = [];
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

    //public class Dates
    //{
    //    public string maximum { get; set; }
    //    public string minimum { get; set; }
    //}


}

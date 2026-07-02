using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Constants
{
    public static class FooterBrands
    {
        public static readonly FooterBrand MagicSquareCode = new FooterBrand
        {
            Label = "Powered By",

            Image = "/images/Home/Branding_Img_3F_NBC.png",

            Width = 135,

            Height = 55,

            Alt = "The Magic Square Code Footer Brand",

            Url = "/"
        };

        public static readonly FooterBrand MovieTime = new FooterBrand
        {
            Label = "Powered By",

            Image = "/images/MovieTime/tmdb_long_logo.png",

            Width = 175,

            Height = 15,

            Alt = "The Movie Database Brand",

            Url = "https://www.themoviedb.org/"
        };

        public static readonly FooterBrand NWSDashboard = new FooterBrand
        {
            Label = "Powered By",

            Image = "/images/NWSDashboard/poweredbyNWS.png",

            Width = 135,

            Height = 45,

            Alt = "National Weather Service Brand",

            Url = "https://www.weather.gov/"
        };
    }
}

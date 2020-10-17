using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieRatingWebApp.Models
{
    public class PopularViewModel
    {
        public Movie MostPopular { get; set; }
        public List<Movie> PopularMovies { get; set; }
    }
}

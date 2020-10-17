using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieRatingWebApp.Models
{
    public class MovieViewModel
    {
        public List<Movie> Movies { get; set; }
        public SelectList Genres { get; set; }
        public int MovieGenre { get; set; }
        public string SearchString { get; set; }
    }
}

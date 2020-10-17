using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieRatingWebApp.Models
{
    public class DirectorViewModel
    {
        public List<Movie> Directors { get; set; }
        public IEnumerable<SelectListItem> Movies { get; set; }
    }
}

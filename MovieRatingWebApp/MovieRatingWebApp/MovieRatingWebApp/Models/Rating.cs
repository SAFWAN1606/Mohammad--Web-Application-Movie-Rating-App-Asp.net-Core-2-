using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRatingWebApp.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public int Rate { get; set; }
        public int MovieID { get; set; }
        public Movie movie { get; set; }
    }
}

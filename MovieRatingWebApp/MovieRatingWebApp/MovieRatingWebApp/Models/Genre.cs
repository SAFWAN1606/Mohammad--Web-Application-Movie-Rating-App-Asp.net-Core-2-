using System.ComponentModel.DataAnnotations;

namespace MovieRatingWebApp.Models
{
    public class Genre
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}

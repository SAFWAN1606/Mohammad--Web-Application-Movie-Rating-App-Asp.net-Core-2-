using System.ComponentModel.DataAnnotations;

namespace MovieRatingWebApp.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

    }
}

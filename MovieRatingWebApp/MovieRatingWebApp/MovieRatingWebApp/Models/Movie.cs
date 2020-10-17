using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MovieRatingWebApp.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Display(Name = "Movie Title")]
        [StringLength(80, MinimumLength = 2)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Year")]
        [Required]
        public int ReleaseYear { get; set; }

        [Display(Name = "Poster Image")]
        public string PosterImage{ get; set; }

        [Display(Name = "Scene Image")]
        public string SceneImage { get; set; }


        [Display(Name = "Genre")]
        [Required]
        public string GenreID { get; set; }
        public Genre Genre { get; set; }

        [Display(Name = "Director")]
        public int DirectorID { get; set; }
        public virtual Director Director { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public int TotalOne
        {
            get
            {
                return Ratings == null ? 0 : Ratings.Where(x => x.MovieID == this.ID && x.Rate == 1).Count();
            }
        }

        public int TotalTwo
        {
            get
            {
                return Ratings == null ? 0 : Ratings.Where(x => x.MovieID == this.ID && x.Rate == 2).Count();
            }
        }

        public int TotalThree
        {
            get
            {
                return Ratings == null ? 0 : Ratings.Where(x => x.MovieID == this.ID && x.Rate == 3).Count();
            }
        }

        public int TotalFour
        {
            get
            {
                return Ratings == null ? 0 : Ratings.Where(x => x.MovieID == this.ID && x.Rate == 4).Count();
            }
        }

        public int TotalFive
        {
            get
            {
                return Ratings == null ? 0 : Ratings.Where(x => x.MovieID == this.ID && x.Rate == 5).Count();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRatingWebApp.Data;
using MovieRatingWebApp.Models;

namespace MovieRatingWebApp.Controllers
{
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.ID == id);
        }

        //Save rating data into the database
        [HttpPost]
        public JsonResult SaveRating(int rate, int movieId)
        {
            JsonResult result = Json("0");

             var movie = _context.Movies.Find(movieId);

            if (movie == null)
                return result;

            Rating rt = new Rating();
            rt.Rate = rate;
            rt.MovieID = movieId;

            //save into the database
            _context.Ratings.Add(rt);

            _context.SaveChanges();
           
           result = Json( "R1:" + movie.TotalOne + ",R2:" + movie.TotalTwo  
                             + ",R3:" + movie.TotalThree  + ",R4:" + movie.TotalFour  + ",R5:" + movie.TotalFive);                
           return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRatingWebApp.Data;
using MovieRatingWebApp.Models;

namespace MovieRatingWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ratings = from r in _context.Ratings select r;
            ratings = ratings.Where(x => x.Rate == _context.Ratings.Max(r => r.Rate));
            
            var movies = from m in _context.Movies select m;
            var popularVM = new PopularViewModel
            {
                PopularMovies = await movies.Take(3).ToListAsync()
            };


            if(ratings.Any())
            {
                popularVM.MostPopular = _context.Movies.Find(ratings.First().MovieID);
            }
            else
            {
                popularVM.MostPopular = new Movie();
            }
                       
            return View(popularVM);

        }
        public IActionResult Help()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

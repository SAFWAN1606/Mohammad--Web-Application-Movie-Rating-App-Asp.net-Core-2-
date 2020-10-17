using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRatingWebApp.Data;
using MovieRatingWebApp.Models;

namespace MovieRatingWebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public MoviesController(ApplicationDbContext context, IHostingEnvironment appEnv)
        {
            _context = context;
            _appEnvironment = appEnv;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            //return View(await _context.Movie.ToListAsync());
            IQueryable<Genre> genreQuery = from m in _context.Genres
                                            orderby m.Name
                                            select m;

            var movies = from m in _context.Movies select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.GenreID == movieGenre);
            }

            movies = movies.Include(r => r.Ratings);

            movies = movies.Include(a => a.MovieActors);

            var movieVM = new MovieViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync(), "ID", "Name"),
                Movies = await movies.ToListAsync()
            };

            return View(movieVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(a => a.MovieActors)
                .Include(r => r.Ratings)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name");

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie) /* [Bind("ID,Title,ReleaseYear,Genre,PosterImage")] */
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);

                string rootPath = _appEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var movieFromDb = _context.Movies.Find(movie.ID);

                if(files.Count != 0)
                {
                    var uploads = Path.Combine(rootPath, @"images");
                    var extension = Path.GetExtension(files[0].FileName);
                    
                    using (var filestream = new FileStream(Path.Combine(uploads, movie.ID + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    movieFromDb.PosterImage = @"/" + @"images" + @"/" + movie.ID + extension;
                }
                else
                {
                    var uploads = Path.Combine(rootPath, @"images" + @"/" + "default-image.png");
                    System.IO.File.Copy(uploads, rootPath + @"/" + @"images" + @"/" + movie.ID + ".png");
                    movieFromDb.PosterImage = @"/" + @"images" + @"/" + movie.ID + ".png";
                }
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
                
            }

            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "Name", movie.DirectorID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name", movie.GenreID);

            return View(movie);


        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseYear,Genre,PosterImage")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }

  
    }
}

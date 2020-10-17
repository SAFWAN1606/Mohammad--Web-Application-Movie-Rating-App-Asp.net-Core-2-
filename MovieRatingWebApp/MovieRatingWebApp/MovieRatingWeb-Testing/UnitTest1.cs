using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MpvirRatingWebApp;

namespace MovieRating_UnitTestProject
{
    [TestClass]
    public class HomeController
    {
        [TestMethod]
        public async Task<IActionResult> Index()
        {
            rtating r = new rtating();
            r.RatingSelect = "movie";
            Assert.AreEqual("movie ", r.RatingSelect);
        }
    }

    [TestClass]
    public class RegisterViewModel
    {
        [TestMethod]
        public string Email
        {

            // get  = new MailMessage("me@gmail.com", "me@yahoo.com", "My Message Subject", "This is a test message");

        }
    }
    [TestClass]
    public class ManageController : Controller
    {
        [TestMethod]
        public ManageController()
        {
            userManager = new userManager();
            signInManager = new signInManager();
            emailSender = new emailSender();
            logger = new logger();
            urlEncoder = new urlEncoder();
        }


    }

    [TestClass]
    public class MoviesController : Controller
    {
        [TestMethod]
        public async Task<IActionResult> Delete()
        {
            MovieDetails = new MovieDeltails();
            MovieDetails.remove = true;
        }
    }
    [TestClass]
    public class MoviesController : Controller
    {
        [TestMethod]
        public IActionResult Create()
        {
            viewData DirectorID = DirectorID();
            Assert.AreEqual("id", "Name");

        }
    }

    [TestClass]
    public class HomeController : Controller
    {
        [TestMethod]
        public IActionResult Error()
        {
            Assert.AreEqual("Error please try again");

        }
    }
}



using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET api to populate DB
        [HttpGet("populate")]
        public void populate()
        {
            populateUsers();
            populateCategories();
            populateVenues();
            populateReviews();
        }

        // public void depopulate()
        // {
        //     _dbContext.User.RemoveRange(_dbContext.User);
        //     _dbContext.Category.RemoveRange(_dbContext.Category);
        //     _dbContext.Venue.RemoveRange(_dbContext.Venue);
        //     _dbContext.Review.RemoveRange(_dbContext.Review);
        // }


        private void populateUsers()
        {
            User newUserOne = new User 
            {
                FirstName = "Bill",
                LastName = "Billingson",
            };
            _dbContext.Add(newUserOne);
            User newUserTwo = new User 
            {
                FirstName = "Bob",
                LastName = "The family man",
            };
            _dbContext.Add(newUserTwo);
            _dbContext.SaveChanges();
        }
        private void populateVenues()
        {
            Venue newVenueOne = new Venue 
            {
                VenueName = "Mc donalds",
                UserId = 1,
                CategoryId = 1,
                City = "Christchurch",
                ShortDescription  = "Miccy D's",
                LongDescription = "Mac donalds resturant",
                DateAdded = new System.DateTime(),
                Latitude = 38.8951, 
                Longitude = -77.0364
            };
            _dbContext.Add(newVenueOne);
            _dbContext.SaveChanges();
        }
        private void populateReviews()
        {
            Review newReview = new Review 
            {
                VenueId = 1,
                UserId =  1,
                ReviewBody = "A terrible place to take the family",
                StarRating = 2,
                CostRating = 3,
                TimePosted = new System.DateTime()
            };
            Review newReview2 = new Review 
            {
                VenueId = 1,
                UserId =  2,
                ReviewBody = "A terrible place to take the family",
                StarRating = 2,
                CostRating = 3,
                TimePosted = new System.DateTime()
            };
            _dbContext.Add(newReview);
            _dbContext.Add(newReview2);
            _dbContext.SaveChanges();
        }
        private void populateCategories()
        {
            Category newCategoryOne = new Category 
            {
                CategoryName = "Restaurant",
                CategoryDescription = "A place to eat"
            };
            _dbContext.Add(newCategoryOne);
            _dbContext.SaveChanges();
        }  
    }
}
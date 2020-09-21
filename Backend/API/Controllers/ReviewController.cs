using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        
        private readonly ApplicationDbContext _dbContext;
        //set up constructor to build DB
        public ReviewController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // // GET api /venue/review/id
        [HttpGet("venue/review/{id}")]
        public async Task<ActionResult> GetVenueReviews(int id) 
        {
            List<Review> venueReviews = await _dbContext.Review.Where(r => r.VenueId == id).ToListAsync();
            if (venueReviews.Count == 0) {
                return NotFound();
            }
            return Ok(venueReviews);
        }

        // Get api /user/review/id
        [HttpGet("user/review/{id}")]
        public async Task<ActionResult> GetUserReviews(int id)
        {
            List<Review> userReviews = await _dbContext.Review.Where(r => r.UserId == id).ToListAsync();
            if( userReviews.Count == 0 ){
                return NotFound();
            }
            return Ok(userReviews);
        }

        // POST api/review
        [HttpPost("Review")]
        public async Task<ActionResult> PostReview(Review review)
        {
            List<Review> checkForDefaultList = await _dbContext.Review.Where(r => r.VenueId == review.VenueId).ToListAsync();
            Review checkForDefault = checkForDefaultList[0];
            if(checkForDefault.ReviewBody == "Default review" && checkForDefault.StarRating == 0 && checkForDefault.CostRating == 0) 
            {
                _dbContext.Review.Remove(checkForDefault);
            }
            review.TimePosted = DateTime.UtcNow;
            await _dbContext.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return Ok(review);
        } 
    }    
}
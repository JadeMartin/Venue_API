using API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        
        private readonly ApplicationDbContext _dbContext;
        //set up constructor to build DB
        public VenueController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api /venue/id
        [HttpGet("Venue/{id}")]
        public async Task<ActionResult> GetVenue(int id) 
        {
            var venue = await _dbContext.Venue.FindAsync(id);
            if(venue == null) {
                return NotFound();
            }
        return Ok(venue);
        }

        // GET api /venue 
        // Search function will need to take in multiple params
        [HttpGet("Venue")]
        public async Task<ActionResult<IEnumerable<Result>>> Search(Search search) 
        {
            var query = _dbContext.Venue
                .Join(
                    _dbContext.Review.GroupBy(r => r.VenueId).Select(g => new {VenueId = g.Key, AvgStarRating = g.Average( r => r.StarRating), AvgPrice = g.Average( r => r.CostRating) }),
                    v => v.VenueId,
                    r => r.VenueId,
                    (v, r) => new
                      Result {
                        venue_id = v.VenueId,
                        venue_name = v.VenueName,
                        user_id = v.UserId,
                        category_id = v.CategoryId,
                        city = v.City,
                        short_description = v.ShortDescription,
                        long_description = v.LongDescription,
                        latitude = v.Latitude,
                        longitude = v.Longitude,
                        mean_star_rating = r.AvgStarRating,
                        mean_cost_rating = r.AvgPrice
                    });
            
            if(search.City != "")
            {
                query = query.Where(v => v.city == search.City);
            }
            if(search.Q != "")
            {
                query = query.Where(v => v.venue_name.Contains(search.Q));
            }
            if(search.CategoryId > 0)
            {
                query = query.Where(v => v.category_id == search.CategoryId);
            }
            if(search.MaxCostRating > 0)
            {
                query = query.Where(v => v.mean_cost_rating <= search.MaxCostRating);
            }
            if(search.UserId > 0)
            {
                query = query.Where(v => v.user_id == search.UserId);
            }
            if(search.MinStarRating > 0)
            {
                query = query.Where(v => v.mean_star_rating >= search.MinStarRating);
            }

            switch(search.SortBy){
                case "COST_RATING":
                    //ORDER BY mode_cost_rating query.reverseSort ? desc : asc
                    if(search.RevereSort)
                        query = query.OrderBy(r => r.mean_cost_rating);
                    else
                        query = query.OrderByDescending(r => r.mean_cost_rating);
                    break;
                default:
                    //ORDER BY mean_star_rating query.reverseSort ? desc : asc
                    if(search.RevereSort)
                        query = query.OrderBy(r => r.mean_star_rating);
                    else
                        query = query.OrderByDescending(r => r.mean_star_rating);
                    break;

            }
            List<Result> queryResult = await query.ToListAsync();
            return await query.ToListAsync();
        }
        
        // POST api/user
        [HttpPost("Venue")]
        public async Task<ActionResult> PostVenue(Venue venue)
        {
            await _dbContext.AddAsync(venue);
            await _dbContext.SaveChangesAsync();
            await _dbContext.AddAsync(new Review{
                VenueId = venue.VenueId,
                UserId = venue.UserId,
                ReviewBody = "Default review",
                StarRating = 0,
                CostRating = 0,
                TimePosted = new System.DateTime(),
            });
            await _dbContext.SaveChangesAsync();
            return Ok(venue);
        }

        // PUT api/user
        [HttpPut("Venue/{id}")]
        public async Task<ActionResult> PutVenue(Venue venue, int id)
        {
            if (id != venue.VenueId)
            {
                return BadRequest();
            }
            _dbContext.Entry(venue).State = EntityState.Modified;

            try 
            {
                await _dbContext.SaveChangesAsync();
            } 
            catch(DbUpdateConcurrencyException)
            {
                if(!VenueExists(venue.VenueId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(venue);
        }

        private bool VenueExists(int id)
        {
            return _dbContext.Venue.Any(f => f.VenueId == id);
        }
    }    
}
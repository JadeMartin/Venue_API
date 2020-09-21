using API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly ApplicationDbContext _dbContext;
        //set up constructor to build DB
        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        //GET: api/Users
        [HttpGet("User")] 
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await _dbContext.User.ToListAsync();

        // GET api /user/id
        [HttpGet("User/{id}")]
        public async Task<ActionResult> GetUser(int id) 
        {
            var user = await _dbContext.User.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/PostUser
        [HttpPost("User")]
        public async Task<ActionResult> PostUser(User user)
        {
            await _dbContext.AddAsync(user); 

            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        // PUT api/user/:id
        [HttpPut("user/{id}")]
        public async Task<IActionResult> PutUser(User user, int id)
        {

            if( id != user.UserId) 
            {
                return BadRequest();
            }

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(user);
        }

        private bool UserExists(long id)
        {
            return _dbContext.User.Any(u => u.UserId == id);
        }

    }    
}
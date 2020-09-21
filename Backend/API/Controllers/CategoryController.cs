using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET api/categorys
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategorys() 
        {
            return await _dbContext.Category.ToListAsync();
        }

        // GET: api/GetCategory/5
        [HttpGet("Category/{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await _dbContext.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST api/user
        [HttpPost("Category")]
        public async Task<ActionResult> PostCategory(Category category)
        {
            await _dbContext.AddAsync(category);

            await _dbContext.SaveChangesAsync();

            return Ok(category);
        }
    }    
}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MovieReviewAPIDBContext _context;

        public UsersController(MovieReviewAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUserInfo()
        {
            return await _context.UserInfo.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.UserInfo.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.UserId)
            {
                return BadRequest();
            }

            
            _context.Entry(users).State = EntityState.Modified;
            _context.Entry(users).Property(x => x.UserId).IsModified = false;
            _context.Entry(users).Property(x => x.NumOfReviews).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // Ignores NumOfReviews provided by the user
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            users.NumOfReviews = 0;

            _context.UserInfo.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.UserInfo.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            // Recalculate AVGUserRating
            
            _context.UserInfo.Remove(users);

            _context.SaveChanges();

            var showids = await _context.UserReviews.Where(c => c.UserId == users.UserId).ToListAsync();
            if (showids.Any())
            {
                foreach (var showid in showids)
                {
                    var tvshows = await _context.TVShows.FindAsync(showid.ShowId);
                    var reviews = await _context.UserReviews.Where(c => c.ShowId == showid.ShowId).ToListAsync();
                    if (tvshows != null)
                    {
                        if (reviews != null)
                        {
                            tvshows.AVGUserRating = reviews.Sum(c => c.UserRating) / reviews.Count;
                        }
                        else
                        {
                            tvshows.AVGUserRating = 0;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.UserInfo.Any(e => e.UserId == id);
        }
    }
}

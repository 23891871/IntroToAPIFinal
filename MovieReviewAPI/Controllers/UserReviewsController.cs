using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReviewsController : ControllerBase
    {
        private readonly MovieReviewAPIDBContext _context;

        public UserReviewsController(MovieReviewAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/UserReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReviews>>> GetUserReviews()
        {
            var userReviews = await _context.UserReviews.ToListAsync();
            return Ok(new Response(200, "User Reviews", userReviews));
        }

        // Get based on ReviewId, ShowId, or UserId
        // Specific which in the keyword before the id
        // GET: api/UserReviews/reviews/5
        [HttpGet("{type}/{id}")]
        public async Task<ActionResult<UserReviews>> GetUserReviews(string type, int id)
        {
            var userReviews = await _context.UserReviews.ToListAsync();

            if (userReviews == null)
            {
                return NotFound(new Response(404, "User Review with UserId of " + id));
            }

            switch (type)
            {
                case "shows":
                    {
                        var shows = await _context.UserReviews.Where(c => c.ShowId == id).ToListAsync();
                        if (shows != null)
                        {
                            return Ok(new Response(200, "UserReviews of ShowId " + id, shows));
                        }
                        return NotFound(new Response(404, "UserReviews of ShowId " + id));
                    }
                case "users":
                    {
                        var users = await _context.UserReviews.Where(c => c.UserId == id).ToListAsync();
                        if (users != null)
                        {
                            return Ok(new Response(200, "UserReviews of UserId " + id, users));
                        }
                        return NotFound(new Response(404, "UserReviews of UserId " + id));
                    }
                default:
                    {
                        return NotFound(new Response(404, "Data "));
                    }
            }
        }

        // GET: api/UserReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReviews>> GetUserReviews(int id)
        {
            var reviews = await _context.UserReviews.FindAsync(id);
            if (reviews != null)
            {
                return Ok(new Response(200, "UserReview of ReviewId " + id, reviews));
            }
            return NotFound(new Response(404, "UserReview of ReviewId " + id));
        }

        // PUT: api/UserReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserReviews(int id, UserReviews userReviews)
        {
            if (id != userReviews.ReviewId)
            {
                return BadRequest(new Response(400));
            }

            _context.Entry(userReviews).State = EntityState.Modified;
            _context.Entry(userReviews).Property(x => x.UserId).IsModified = false;
            _context.Entry(userReviews).Property(x => x.ReviewId).IsModified = false;
            _context.Entry(userReviews).Property(x => x.ShowId).IsModified = false;

            // Update AVGUserRatings in TVShows
            var tvshows = await _context.TVShows.FindAsync(userReviews.ShowId);
            var reviews = await _context.UserReviews.Where(c => c.ShowId == userReviews.ShowId).ToListAsync();
            if (tvshows != null)
            {
                if (reviews != null)
                {
                    tvshows.AVGUserRating = reviews.Sum(c => c.UserRating) / reviews.Count;
                }
            }
            // End update

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserReviewsExists(id, userReviews.ShowId, userReviews.UserId))
                {
                    return NotFound(new Response(404, "User Review"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserReviews>> PostUserReviews(UserReviews userReviews)
        {
            _context.UserReviews.Add(userReviews);

            // Increase number of user's reviewss
            var user = _context.UserInfo.Find(userReviews.UserId);
            if (user != null)
            {
                user.NumOfReviews += 1;
            }   
            
            try
            {
                _context.SaveChanges();
                // Update AVGUserRatings in TVShows
                var tvshows = await _context.TVShows.FindAsync(userReviews.ShowId);
                var reviews = await _context.UserReviews.Where(c => c.ShowId == userReviews.ShowId).ToListAsync();
                if (tvshows != null)
                {
                    if (reviews != null)
                    {
                        tvshows.AVGUserRating = (reviews.Sum(c => c.UserRating) / reviews.Count);
                    }
                }
                // End update
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (UserReviewsExists(userReviews.ReviewId, userReviews.ShowId, userReviews.UserId))
                    {
                        return Conflict(new Response(409));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (DbUpdateException)
            {
                if (UserReviewsExists(userReviews.ReviewId, userReviews.ShowId, userReviews.UserId))
                {
                    return Conflict(new Response(409));
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserReviews", new { id = userReviews.ReviewId }, new Response(201, "User Review", userReviews));
        }

        // DELETE: api/UserReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserReviews(int id)
        {
            var userReviews = await _context.UserReviews.FindAsync(id);
            if (userReviews == null)
            {
                return NotFound(new Response(404, "User Review of User Id "));
            }

            var user = await _context.UserInfo.FindAsync(userReviews.UserId);
            if (user != null)
            {
                user.NumOfReviews -= 1;
            }

            _context.UserReviews.Remove(userReviews);

            _context.SaveChanges();

            var tvshows = await _context.TVShows.FindAsync(userReviews.ShowId);
            var reviews = await _context.UserReviews.Where(c => c.ShowId == userReviews.ShowId).ToListAsync();
            if (tvshows != null)
            {
                if (reviews != null)
                {
                    tvshows.AVGUserRating = reviews.Sum(c => c.UserRating) / reviews.Count;
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserReviewsExists(int rid, int sid, int uid)
        {
            return _context.UserReviews.Any(e => e.ReviewId == rid || (e.ShowId == sid && e.UserId == uid));
        }
    }
}

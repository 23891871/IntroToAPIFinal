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
            return await _context.UserReviews.ToListAsync();
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
                return NotFound();
            }

            if (type == "reviews")
            {
                return Ok(await _context.UserReviews.Where(c => c.ReviewId == id).ToListAsync());
                
            }
            else if (type == "shows")
            {
                return Ok(await _context.UserReviews.Where(c => c.ShowId == id).ToListAsync());
            }
            else if (type == "users")
            {
                return Ok(await _context.UserReviews.Where(c => c.UserId == id).ToListAsync());
            }
            return NotFound();
        }

        // GET: api/UserReviews/5/5/5
        // rid = ReviewId, sid = ShowId, uid = UserId
        [HttpGet("{rid}/{sid}/{uid}")]
        public async Task<ActionResult<UserReviews>> GetUserReviews(int rid, int sid, int uid)
        {
            var userReviews = await _context.UserReviews.FindAsync(rid, sid, uid);

            if (userReviews == null)
            {
                return NotFound();
            }

            return userReviews;
        }

        // PUT: api/UserReviews/5/5/5
        // rid = ReviewId, sid = ShowId, uid = UserId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{rid}/{sid}/{uid}")]
        public async Task<IActionResult> PutUserReviews(int rid, int sid, int uid, UserReviews userReviews)
        {
            if (rid != userReviews.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(userReviews).State = EntityState.Modified;
            _context.Entry(userReviews).Property(x => x.UserId).IsModified = false;
            _context.Entry(userReviews).Property(x => x.ReviewId).IsModified = false;
            _context.Entry(userReviews).Property(x => x.ShowId).IsModified = false;

            var tvshows = await _context.TVShows.FindAsync(userReviews.ShowId);
            var reviews = await _context.UserReviews.Where(c => c.ShowId == userReviews.ShowId).ToListAsync();
            if (tvshows != null)
            {
                if (reviews != null)
                {
                    tvshows.AVGUserRating = reviews.Sum(c => c.UserRating) / reviews.Count;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserReviewsExists(rid, sid, uid))
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

        // POST: api/UserReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserReviews>> PostUserReviews(UserReviews userReviews)
        {
            _context.UserReviews.Add(userReviews);

            var user = _context.UserInfo.Find(userReviews.UserId);
            if (user != null)
            {
                user.NumOfReviews += 1;
            }   
            
            try
            {
                _context.SaveChanges();
                var tvshows = await _context.TVShows.FindAsync(userReviews.ShowId);
                var reviews = await _context.UserReviews.Where(c => c.ShowId == userReviews.ShowId).ToListAsync();
                if (tvshows != null)
                {
                    if (reviews != null)
                    {
                        tvshows.AVGUserRating = (reviews.Sum(c => c.UserRating) / reviews.Count);
                    }
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (UserReviewsExists(userReviews.ReviewId, userReviews.ShowId, userReviews.UserId))
                    {
                        return Conflict();
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
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserReviews", new { id = userReviews.ReviewId }, userReviews);
        }

        // DELETE: api/UserReviews/5/5/5
        // rid = ReviewId, sid = ShowId, uid = UserId
        [HttpDelete("{rid}/{sid}/{uid}")]
        public async Task<IActionResult> DeleteUserReviews(int rid, int sid, int uid)
        {
            var userReviews = await _context.UserReviews.FindAsync(rid, sid, uid);
            if (userReviews == null)
            {
                return NotFound();
            }

            var user = await _context.UserInfo.FindAsync(uid);
            if (user != null)
            {
                user.NumOfReviews -= 1;
            }

            _context.UserReviews.Remove(userReviews);

            _context.SaveChanges();

            var tvshows = await _context.TVShows.FindAsync(sid);
            var reviews = await _context.UserReviews.Where(c => c.ShowId == sid).ToListAsync();
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
            
            return _context.UserReviews.Any(e => e.ReviewId == rid && e.ShowId == sid && e.UserId == uid);
        }
    }
}

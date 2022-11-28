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
    public class TVShowsController : ControllerBase
    {
        private readonly MovieReviewAPIDBContext _context;

        public TVShowsController(MovieReviewAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/TVShows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TVShows>>> GetTVShows()
        {
            return await _context.TVShows.ToListAsync();
        }

        // GET: api/TVShows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TVShows>> GetTVShows(int id)
        {
            var tVShows = await _context.TVShows.FindAsync(id);
            if (tVShows == null)
            {
                return NotFound();
            }

            return tVShows;
        }

        // GET: api/TVShows/IMDB/5
        [HttpGet("top/{database}/{num}")]
        public async Task<ActionResult<TVShows>> GetTVShows(string database, int num)
        {
            if (database == "IMDB")
            {
                return Ok(await _context.TVShows.OrderByDescending(c => c.IMDBrating).Take(num).ToListAsync());
            } else if (database == "RT")
            {
                return Ok(await _context.TVShows.OrderByDescending(c => c.RTrating).Take(num).ToListAsync());
            } else if (database == "User")
            {
                return Ok(await _context.TVShows.OrderByDescending(c => c.AVGUserRating).Take(num).ToListAsync());
            }

            return NotFound();
        }

        // GET: api/TVShows/genre/Action
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<TVShows>> GetTVShows(string genre)
        {
            var tVShows = await _context.TVShows.Where(c => c.Genre == genre).ToListAsync();
            if (tVShows != null)
            {
                return Ok(tVShows);
            }

            return NotFound();
        }

        // PUT: api/TVShows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTVShows(int id, TVShows tVShows)
        {
            if (id != tVShows.ShowId)
            {
                return BadRequest();
            }

            _context.Entry(tVShows).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TVShowsExists(id))
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

        // POST: api/TVShows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TVShows>> PostTVShows(TVShows tVShows)
        {
            _context.TVShows.Add(tVShows);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTVShows", new { id = tVShows.ShowId }, tVShows);
        }

        // DELETE: api/TVShows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTVShows(int id)
        {
            var tVShows = await _context.TVShows.FindAsync(id);
            if (tVShows == null)
            {
                return NotFound();
            }

            _context.TVShows.Remove(tVShows);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TVShowsExists(int id)
        {
            return _context.TVShows.Any(e => e.ShowId == id);
        }
    }
}

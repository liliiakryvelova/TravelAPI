using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Travel1API.Models;
#nullable disable

namespace Travel1API
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly Travel1APIContext _db;

    public ReviewsController(Travel1APIContext db)
    {
      _db = db;
    }

    //Get api/Reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> Get()
    { 
      return await _db.Reviews.ToListAsync();
    }

    // GET: api/Reviews?destination=London&groupby=rating
    [HttpGet("destination")]
    public async Task<ActionResult<IEnumerable<Review>>> GetDestination(string destination, string orderby)//"London"
    {
      var query = _db.Reviews.AsQueryable();
   
      if( orderby != null)
      {
          query = query.OrderByDescending(r => r.Rating);
      }
      if (destination != null)
      {
         query =  from r in _db.Reviews
                    join d in _db.Destinations
                    on r.DestinationId equals d.DestinationId
                    where d.DestinationName == destination
                    select r;

      }
        return await query.ToListAsync();
    }
    // GET: api/Reviews?overall=rating
    [HttpGet("overall")]
    public async Task<ActionResult<IEnumerable<DestinationByRating>>> GetReviews(string overall)
    {
      if(overall != null)
      {
          var joinResult = from r in _db.Reviews
              join d in _db.Destinations on r.DestinationId
              equals d.DestinationId
              orderby r.Rating
              select new DestinationByRating(
              d.DestinationName,
              r.Rating); 
              return await joinResult.ToListAsync();
      }
      else{
        var unsortedResult = from r in _db.Reviews
                    join d in _db.Destinations on r.DestinationId
                    equals d.DestinationId
                    select new DestinationByRating(
                    d.DestinationName,
                    r.Rating);
          return await unsortedResult.ToListAsync();
        }
      
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetUser(int id)
    {
        var review = await _db.Reviews.FindAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }
    // POST api/reviews
    [HttpPost]
    public async Task<ActionResult<Review>> Post(Review review)
    {
      _db.Reviews.Add(review);
      await _db.SaveChangesAsync();

      //return CreatedAction("GetTravelAPI", new { id = user.UserId }, user})
      return CreatedAtAction("POST", new { id = review.ReviewId }, review);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Review review)
    {
        if (id != review.ReviewId)
        {
            return BadRequest();
        }

        _db.Entry(review).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReviewExists(id))
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
    private bool ReviewExists(int id)
    {
      return _db.Reviews.Any(e => e.ReviewId == id);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var review = await _db.Reviews.FindAsync(id);
      if (review == null)
      {
        return NotFound();
      }

      _db.Reviews.Remove(review);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}
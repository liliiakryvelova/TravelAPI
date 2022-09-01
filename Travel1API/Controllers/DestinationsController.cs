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
  public class DestinationsController : ControllerBase
  {
    private readonly Travel1APIContext _db;

    public DestinationsController(Travel1APIContext db)
    {
      _db = db;
    }

    //Get api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Destination>>> Get(string destination)
    { //var arr = new List<string> {"I done"};

      var query = _db.Destinations.AsQueryable();

      if(destination != null)
      {
        query = query.Where(entry => entry.DestinationName == destination);
      }
      //return await _db.Users.ToListAsync();
      //return arr;
      return await query.ToListAsync();
    }

    // POST api/users
    [HttpPost]
    public async Task<ActionResult<Destination>> Post(Destination destination)
    {
      _db.Destinations.Add(destination);
      await _db.SaveChangesAsync();

      //return CreatedAction("GetTravelAPI", new { id = user.UserId }, user})
      return CreatedAtAction("POST", new { id = destination.DestinationId }, destination);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
        var destination = await _db.Destinations.FindAsync(id);

        if (destination == null)
        {
            return NotFound();
        }

        return destination;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Destination destination)
    {
        if (id != destination.DestinationId)
        {
            return BadRequest();
        }

        _db.Entry(destination).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DestinationExists(id))
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
    private bool DestinationExists(int id)
    {
      return _db.Destinations.Any(e => e.DestinationId == id);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var destination = await _db.Destinations.FindAsync(id);
      if (destination == null)
      {
        return NotFound();
      }

      _db.Destinations.Remove(destination);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}

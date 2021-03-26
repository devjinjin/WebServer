using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models.Places;

namespace WebServer.Controllers.Place
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceKeepController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaceKeepController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PlaceKeep
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceKeep>>> GetPlaceKeep()
        {
            return await _context.PlaceKeep.ToListAsync();
        }

        // GET: api/PlaceKeep/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceKeep>> GetPlaceKeep(int id)
        {
            var placeKeep = await _context.PlaceKeep.FindAsync(id);

            if (placeKeep == null)
            {
                return NotFound();
            }

            return placeKeep;
        }

        // PUT: api/PlaceKeep/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceKeep(int id, PlaceKeep placeKeep)
        {
            if (id != placeKeep.Id)
            {
                return BadRequest();
            }

            _context.Entry(placeKeep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceKeepExists(id))
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

        // POST: api/PlaceKeep
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlaceKeep>> PostPlaceKeep(PlaceKeep placeKeep)
        {
            _context.PlaceKeep.Add(placeKeep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceKeep", new { id = placeKeep.Id }, placeKeep);
        }

        // DELETE: api/PlaceKeep/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaceKeep>> DeletePlaceKeep(int id)
        {
            var placeKeep = await _context.PlaceKeep.FindAsync(id);
            if (placeKeep == null)
            {
                return NotFound();
            }

            _context.PlaceKeep.Remove(placeKeep);
            await _context.SaveChangesAsync();

            return placeKeep;
        }

        private bool PlaceKeepExists(int id)
        {
            return _context.PlaceKeep.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendTravelApp.Models;

namespace BackendTravelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristicObjectiveController : ControllerBase
    {
        private readonly TravelContext _context;

        public TouristicObjectiveController(TravelContext context)
        {
            _context = context;
        }

        // GET: api/TouristicObjective
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouristicObjective>>> GetTouristicObjective()
        {
            return await _context.TouristicObjective.ToListAsync();
        }

        // GET: api/TouristicObjective/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TouristicObjective>> GetTouristicObjective(int id)
        {
            var touristicObjective = await _context.TouristicObjective.FindAsync(id);

            if (touristicObjective == null)
            {
                return NotFound();
            }

            return touristicObjective;
        }

        // PUT: api/TouristicObjective/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTouristicObjective(int id, TouristicObjective touristicObjective)
        {
            if (id != touristicObjective.ObjectiveId)
            {
                return BadRequest();
            }

            _context.Entry(touristicObjective).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TouristicObjectiveExists(id))
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

        // POST: api/TouristicObjective
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TouristicObjective>> PostTouristicObjective(TouristicObjective touristicObjective)
        {
            _context.TouristicObjective.Add(touristicObjective);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTouristicObjective", new { id = touristicObjective.ObjectiveId }, touristicObjective);
        }

        // DELETE: api/TouristicObjective/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTouristicObjective(int id)
        {
            var touristicObjective = await _context.TouristicObjective.FindAsync(id);
            if (touristicObjective == null)
            {
                return NotFound();
            }

            _context.TouristicObjective.Remove(touristicObjective);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TouristicObjectiveExists(int id)
        {
            return _context.TouristicObjective.Any(e => e.ObjectiveId == id);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prometheus.Api.Data;
using Prometheus.Api.Models;

namespace Prometheus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevenuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RevenuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Revenues
        // This method retrieves a list of all revenues.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Revenue>>> GetRevenues()
        {
            return await _context.Revenues
                .Include(r => r.Project)
                .Include(r => r.Invoice)
                .ToListAsync();
        }

        // GET: api/Revenues/5
        // This method retrieves a specific revenue by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Revenue>> GetRevenue(int id)
        {
            var revenue = await _context.Revenues
                .Include(r => r.Project)
                .Include(r => r.Invoice)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (revenue == null)
            {
                return NotFound();
            }

            return revenue;
        }

        // PUT: api/Revenues/5
        // This method updates an existing revenue by ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRevenue(int id, Revenue revenue)
        {
            if (id != revenue.Id)
            {
                return BadRequest();
            }

            _context.Entry(revenue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueExists(id))
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

        // POST: api/Revenues
        // This method creates a new revenue.
        [HttpPost]
        public async Task<ActionResult<Revenue>> CreateRevenue(Revenue revenue)
        {
            _context.Revenues.Add(revenue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRevenue), new { id = revenue.Id }, revenue);
        }

        // DELETE: api/Revenues/5
        // This method deletes a specific revenue by ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevenue(int id)
        {
            var revenue = await _context.Revenues.FindAsync(id);
            if (revenue == null)
            {
                return NotFound();
            }

            _context.Revenues.Remove(revenue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a revenue with the given ID exists.
        private bool RevenueExists(int id)
        {
            return _context.Revenues.Any(r => r.Id == id);
        }
    }
}
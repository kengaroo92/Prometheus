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
    public class ProposalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProposalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proposals
        // This method retrieves a list of all proposals.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposal>>> GetProposals()
        {
            return await _context.Proposals
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .ToListAsync();
        }

        // GET: api/Proposals/5
        // This method retrieves a specific proposal by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Proposal>> GetProposal(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proposal == null)
            {
                return NotFound();
            }

            return proposal;
        }

        // PUT: api/Proposals/5
        // This method updates an existing proposal by ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposal(int id, Proposal proposal)
        {
            if (id != proposal.Id)
            {
                return BadRequest();
            }

            _context.Entry(proposal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalExists(id))
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

        // POST: api/Proposals
        // This method creates a new proposal.
        [HttpPost]
        public async Task<ActionResult<Proposal>> CreateProposal(Proposal proposal)
        {
            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProposal), new { id = proposal.Id }, proposal);
        }

        // DELETE: api/Proposals/5
        // This method deletes a specific proposal by ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposal(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a proposal with the given ID exists.
        private bool ProposalExists(int id)
        {
            return _context.Proposals.Any(e => e.Id == id);
        }
    }
}
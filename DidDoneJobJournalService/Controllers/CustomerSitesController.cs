using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DidDoneJobJournalService.Models.DB;
using DidDoneListModels;

namespace DidDoneJobJournalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSitesController : ControllerBase
    {
        private readonly DidDoneContext _context;

        public CustomerSitesController(DidDoneContext context)
        {
            _context = context;
        }

        // GET: api/CustomerSites
        [HttpGet]
        public IEnumerable<CustomerSite> GetCustomerSite()
        {
            return _context.CustomerSite;
        }

        // GET: api/CustomerSites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerSite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerSite = await _context.CustomerSite.FindAsync(id);

            if (customerSite == null)
            {
                return NotFound();
            }

            return Ok(customerSite);
        }

        // PUT: api/CustomerSites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerSite([FromRoute] int id, [FromBody] CustomerSite customerSite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerSite.CustomerSiteId)
            {
                return BadRequest();
            }

            _context.Entry(customerSite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerSiteExists(id))
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

        // POST: api/CustomerSites
        [HttpPost]
        public async Task<IActionResult> PostCustomerSite([FromBody] CustomerSite customerSite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerSite.Add(customerSite);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerSiteExists(customerSite.CustomerSiteId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerSite", new { id = customerSite.CustomerSiteId }, customerSite);
        }

        // DELETE: api/CustomerSites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerSite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerSite = await _context.CustomerSite.FindAsync(id);
            if (customerSite == null)
            {
                return NotFound();
            }

            _context.CustomerSite.Remove(customerSite);
            await _context.SaveChangesAsync();

            return Ok(customerSite);
        }

        private bool CustomerSiteExists(int id)
        {
            return _context.CustomerSite.Any(e => e.CustomerSiteId == id);
        }
    }
}
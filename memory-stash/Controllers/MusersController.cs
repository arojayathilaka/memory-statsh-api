using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using memory_stash.Models;

namespace memory_stash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusersController : ControllerBase
    {
        private readonly MemoryStashDbContext _context;

        public MusersController(MemoryStashDbContext context)
        {
            _context = context;
        }

        // GET: api/Musers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Muser>>> GetMusers()
        {
            return await _context.Musers.ToListAsync();
        }

        // GET: api/Musers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Muser>> GetMuser(int id)
        {
            var muser = await _context.Musers.FindAsync(id);

            if (muser == null)
            {
                return NotFound();
            }

            return muser;
        }

        // PUT: api/Musers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMuser(int id, Muser muser)
        {
            if (id != muser.Id)
            {
                return BadRequest();
            }

            _context.Entry(muser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MuserExists(id))
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

        // POST: api/Musers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Muser>> PostMuser(Muser muser)
        {
            _context.Musers.Add(muser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MuserExists(muser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMuser", new { id = muser.Id }, muser);
        }

        // DELETE: api/Musers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMuser(int id)
        {
            var muser = await _context.Musers.FindAsync(id);
            if (muser == null)
            {
                return NotFound();
            }

            _context.Musers.Remove(muser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MuserExists(int id)
        {
            return _context.Musers.Any(e => e.Id == id);
        }
    }
}

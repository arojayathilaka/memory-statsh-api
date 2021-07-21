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
    public class MemoriesController : ControllerBase
    {
        private readonly MemoryStashDbContext _context;

        public MemoriesController(MemoryStashDbContext context)
        {
            _context = context;
        }

        // GET: api/Memories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Memory>>> GetMemories()
        {
            return await _context.Memories.ToListAsync();
        }

        // GET: api/Memories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memory>> GetMemory(int id)
        {
            var memory = await _context.Memories.FindAsync(id);

            if (memory == null)
            {
                return NotFound();
            }

            return memory;
        }

        // PUT: api/Memories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemory(int id, Memory memory)
        {
            if (id != memory.Id)
            {
                return BadRequest();
            }

            _context.Entry(memory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoryExists(id))
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

        // POST: api/Memories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Memory>> PostMemory(Memory memory)
        {
            _context.Memories.Add(memory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemoryExists(memory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMemory", new { id = memory.Id }, memory);
        }

        // DELETE: api/Memories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemory(int id)
        {
            var memory = await _context.Memories.FindAsync(id);
            if (memory == null)
            {
                return NotFound();
            }

            _context.Memories.Remove(memory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemoryExists(int id)
        {
            return _context.Memories.Any(e => e.Id == id);
        }
    }
}

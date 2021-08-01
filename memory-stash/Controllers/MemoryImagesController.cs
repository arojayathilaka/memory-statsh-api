using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using memory_stash.Models;
using memory_stash.Data.Models;

namespace memory_stash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryImagesController : ControllerBase
    {
        private readonly MemoryStashDbContext _context;

        public MemoryImagesController(MemoryStashDbContext context)
        {
            _context = context;
        }

        // GET: api/MemoryImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemoryImage>>> GetMemoryImages()
        {
            return await _context.MemoryImages.ToListAsync();
        }

        // GET: api/MemoryImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemoryImage>> GetMemoryImage(int id)
        {
            var memoryImage = await _context.MemoryImages.FindAsync(id);

            if (memoryImage == null)
            {
                return NotFound();
            }

            return memoryImage;
        }

        [HttpGet("with-memory/{id}")]
        public async Task<ActionResult<MemoryImage>> GetMemoryImageWithMemory(int id)
        {
            //var memoryImage = await  _context.MemoryImages
            //        .Include(img => img.Memory)
            //        .Where(img => img.Id == id)
            //        .FirstOrDefaultAsync();

            var memoryImage = await _context.MemoryImages.SingleAsync(img => img.Id == id);

            _context.Entry(memoryImage)
                .Reference(img => img.Memory)
                .Load();

            if (memoryImage == null)
            {
                return NotFound();
            }

            return memoryImage; 
        }

        // PUT: api/MemoryImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemoryImage(int id, MemoryImage memoryImage)
        {
            if (id != memoryImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(memoryImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoryImageExists(id))
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

        // POST: api/MemoryImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MemoryImage>> PostMemoryImage(MemoryImage memoryImage)
        {
            _context.MemoryImages.Add(memoryImage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemoryImageExists(memoryImage.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMemoryImage", new { id = memoryImage.Id }, memoryImage);
        }

        // DELETE: api/MemoryImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemoryImage(int id)
        {
            var memoryImage = await _context.MemoryImages.FindAsync(id);
            if (memoryImage == null)
            {
                return NotFound();
            }

            _context.MemoryImages.Remove(memoryImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemoryImageExists(int id)
        {
            return _context.MemoryImages.Any(e => e.Id == id);
        }
    }
}

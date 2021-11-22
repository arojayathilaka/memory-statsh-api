using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using memory_stash.Models;
using memory_stash.Data.Services;
using memory_stash.Data.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using memory_stash.Data.Models;

namespace memory_stash.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemoriesController : ControllerBase
    {
        private readonly MemoriesService _memoriesService;
        private readonly GroupsService _mGroupsService;


        public MemoriesController(MemoriesService memoriesService, GroupsService mGroupsService)
        {
            _memoriesService = memoriesService;
            _mGroupsService = mGroupsService;
        }


        // GET: api/Memories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemoryVM>>> GetMemories()
        {
            return await _memoriesService.GetMemories();
        }


        // GET: api/Memories/MemoryGroups
        [HttpGet("MemoryGroups")]
        public async Task<ActionResult<IEnumerable<MemoryGroupVM>>> GetMemoriesWithGroup()
        {
            return await _memoriesService.GetMemoriesWithGroup();
        }


        // GET: api/Memories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memory>> GetMemory(int id)
        {
            var memory = await _memoriesService.GetMemory(id);

            if (memory.Id == 0)
            {
                return NotFound();
            }

            return memory;
        }


        // PUT: api/Memories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemory(int id, MemoryUpdateVM memory)
        {
            if (id != memory.Id || !_mGroupsService.MgroupExists(memory.GroupId))
            {
                return BadRequest();
            }

            try
            {
                await _memoriesService.PutMemory(memory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_memoriesService.MemoryExists(id))
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
        [HttpPost]
        public async Task<ActionResult<Memory>> PostMemory(MemoryAddVM memory)
        {

            try
            {
                await _memoriesService.PostMemory(memory);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction(nameof(PostMemory), new { title = memory.Title }, memory);
        }


        // DELETE: api/Memories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemory(int id)
        {
            if (!_memoriesService.MemoryExists(id))
            {
                return NotFound();
            }

            await _memoriesService.DeleteMemory(id);

            return NoContent();
        }

    }
}

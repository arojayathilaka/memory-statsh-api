﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using memory_stash.Models;
using memory_stash.Data.ViewModels;
using memory_stash.Data.Services;

namespace memory_stash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MgroupsController : ControllerBase
    {
        private readonly MgroupsService _mGroupsService;

        public MgroupsController(MgroupsService mGroupsService)
        {
            _mGroupsService = mGroupsService;
        }

        // GET: api/Mgroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MgroupVM>>> GetMgroups()
        {
            return await _mGroupsService.GetMgroups();
        }

        // GET: api/Mgroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MgroupVM>> GetMgroup(int id)
        {
            var mgroup = await _mGroupsService.GetMgroup(id);

            if (mgroup.Id == 0)
            {
                return NotFound();
            }

            return mgroup;
        }

        // PUT: api/Mgroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMgroup(int id, MgroupVM mgroup)
        {
            if (id != mgroup.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mGroupsService.PutMgroup(mgroup);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_mGroupsService.MgroupExists(id))
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

        // POST: api/Mgroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mgroup>> PostMgroup(MgroupVM mgroup)
        {

            if(mgroup.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                await _mGroupsService.PostMgroup(mgroup);
            }
            catch (DbUpdateException)
            {
                if (_mGroupsService.MgroupExists(mgroup.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMgroup", new { id = mgroup.Id }, mgroup);
        }

        // DELETE: api/Mgroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMgroup(int id)
        {
            //var mgroup = await _mGroupsService.GetMgroup(id);
            if (!_mGroupsService.MgroupExists(id))
            {
                return NotFound();
            }

            await _mGroupsService.DeleteMgroup(id);

            return NoContent();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using memory_stash.Models;
using memory_stash.Data.ViewModels;
using memory_stash.Data.Services;
using memory_stash.Data.Models;

namespace memory_stash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupsService _mGroupsService;

        public GroupsController(GroupsService mGroupsService)
        {
            _mGroupsService = mGroupsService;
        }

        // GET: api/Mgroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupVM>>> GetMgroups()
        {
            return await _mGroupsService.GetMgroups();
        }

        // GET: api/Mgroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupVM>> GetMgroup(int id)
        {
            var group = await _mGroupsService.GetMgroup(id);

            if (group.Name == null)
            {
                return NotFound();
            }

            return group;
        }

        // GET: api/Mgroups/5/memories
        [HttpGet("{id}/memories")]
        public async Task<ActionResult<IEnumerable<MemoryVM>>> GetMgroupMemories(int id)
        {
            return await _mGroupsService.GetMgroupMemories(id);
        }


        // GET: api/Mgroups/5/users
        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<Group_User>>> GetMgroupUsers(int id)
        {
            return await _mGroupsService.GetMgroupUsers(id);
        }

        // PUT: api/Mgroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMgroup(int id, GroupUpdateVM group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mGroupsService.PutMgroup(group);
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
        public async Task<ActionResult<GroupVM>> PostGroup(GroupVM group)
        {

            try
            {
                await _mGroupsService.PostMgroup(group);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction(nameof(PostGroup), new { name = group.Name }, group);
        }


        // POST: api/Mgroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("user")]
        public async Task<ActionResult<GroupVM>> PostMgroupUser(Group_User groupUser)
        {

            if (groupUser.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                await _mGroupsService.PostMgroupUser(groupUser);
            }
            catch (DbUpdateException)
            {
                if (_mGroupsService.MgroupUserExists(groupUser.GroupId, groupUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMgroupUser", new { id = groupUser.Id }, groupUser);
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

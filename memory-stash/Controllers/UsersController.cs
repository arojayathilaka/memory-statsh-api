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
using memory_stash.Data.Models;

namespace memory_stash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _musersService;

        public UsersController(UsersService musersService)
        {
            _musersService = musersService;
        }

        // GET: api/Musers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetMusers()
        {
            return await _musersService.GetMusers();
        }

        // GET: api/Musers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVM>> GetMuser(int id)
        {
            var muser = await _musersService.GetMuser(id);

            if (muser.Id == 0)
            {
                return NotFound();
            }

            return muser;
        }

        // GET: api/Musers/5/group
        [HttpGet("{id}/group")]
        public async Task<ActionResult<IEnumerable<Group_User>>> GetUsersGroup(int id)
        {
            return await _musersService.GetUsersGroup(id);
        }

        // PUT: api/Musers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMuser(int id, UserVM muser)
        {
            if (id != muser.Id)
            {
                return BadRequest();
            }

            try
            {
                await _musersService.PutMuser(muser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_musersService.MuserExists(id))
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
        public async Task<ActionResult<User>> PostMuser(UserVM muser)
        {
            if (muser.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                await _musersService.PostMuser(muser);
            }
            catch (DbUpdateException)
            {
                if (_musersService.MuserExists(muser.Id))
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
            if (!_musersService.MuserExists(id))
            {
                return NotFound();
            }

            await _musersService.DeleteMuser(id);

            return NoContent();
        }

    }
}

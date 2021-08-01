using memory_stash.Data.Models;
using memory_stash.Data.ViewModels;
using memory_stash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace memory_stash.Data.Services
{
    public class MgroupsService
    {
        private readonly MemoryStashDbContext _context;

        public MgroupsService(MemoryStashDbContext context)
        {
            _context = context;
        }

        public async Task<List<MgroupVM>> GetMgroups()
        {
            return await _context.Mgroups
                .Select(m => ConvertToMgroupVM(m))
                .ToListAsync();
        }

        public async Task<MgroupVM> GetMgroup(int id)
        {
            var mgroup = await _context.Mgroups.FindAsync(id);

            return ConvertToMgroupVM(mgroup);
        }
        
        public async Task<List<MemoryVM>> GetMgroupMemories(int id)
        {
            var mgroup = await _context.Mgroups.FindAsync(id);

            _context.Entry(mgroup)
                .Collection(grp => grp.Memories)
                .Load();

            return mgroup.Memories.ToList().Select(m => MemoriesService.ConvertToMeomoryVM(m)).ToList();
        }

        public async Task<List<GroupUser>> GetMgroupUsers(int id)
        {
            var mgroup = await _context.Mgroups.FindAsync(id);

            _context.Entry(mgroup)
                .Collection(grp => grp.GroupUsers)
                .Query()
                .Include(gu => gu.User)
                .Load();

            return mgroup.GroupUsers.ToList();
        }

        public async Task PutMgroup(MgroupVM mgroup)
        {
            var mGroup = ConvertToMgroup(mgroup);
            _context.Entry(mGroup).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task PostMgroup(MgroupVM mgroup)
        {
            var mGroup = ConvertToMgroup(mgroup);
            _context.Mgroups.Add(mGroup); 

            await _context.SaveChangesAsync();
        }

        public async Task PostMgroupUser(GroupUser groupUser)
        {
            //var mGroup = ConvertToMgroup(mgroup);
            _context.GroupUsers.Add(groupUser);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMgroup(int id)
        {
            var mgroup = await _context.Mgroups.FindAsync(id);
            _context.Mgroups.Remove(mgroup);

            await _context.SaveChangesAsync();
        }

        public bool MgroupExists(int id)
        {
            return _context.Mgroups.Any(e => e.Id == id);
        }

        public bool MgroupUserExists(int groupId, int userId)
        {
            return _context.GroupUsers.Any(e => e.GroupId == groupId && e.UserId == userId);
        }

        private static MgroupVM ConvertToMgroupVM(Mgroup mgroup)
        {
            if (mgroup == null)
            {
                return new MgroupVM();
            }

            return new MgroupVM()
            {
                Id = mgroup.Id,
                Name = mgroup.Name,
            };
        }

        private static MgroupMemoryVM ConvertToMgroupMemoryVM(Mgroup mgroup)
        {
            if (mgroup == null)
            {
                return new MgroupMemoryVM();
            }

            var memories = mgroup.Memories.ToList();
            var memoriesVM = memories.Select(m => MemoriesService.ConvertToMeomoryVM(m)).ToList();

            return new MgroupMemoryVM()
            {
                Id = mgroup.Id,
                Name = mgroup.Name,
                Memories = memoriesVM
            };
        }

        private static Mgroup ConvertToMgroup(MgroupVM mgroup)
        {
            return new Mgroup()
            {
                Id = mgroup.Id,
                Name = mgroup.Name
            };
        }
    }
}

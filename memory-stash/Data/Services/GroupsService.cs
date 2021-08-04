using memory_stash.Data.Models;
using memory_stash.Data.ViewModels;
using memory_stash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = memory_stash.Data.Models.Group;

namespace memory_stash.Data.Services
{
    public class GroupsService
    {
        private readonly MemoryStashDbContext _context;

        public GroupsService(MemoryStashDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupVM>> GetMgroups()
        {
            return await _context.Groups
                .Select(m => ConvertToMgroupVM(m))
                .ToListAsync();
        }

        public async Task<GroupVM> GetMgroup(int id)
        {
            var mgroup = await _context.Groups.FindAsync(id);

            return ConvertToMgroupVM(mgroup);
        }
        
        public async Task<List<MemoryVM>> GetMgroupMemories(int id)
        {
            var mgroup = await _context.Groups.FindAsync(id);

            _context.Entry(mgroup)
                .Collection(grp => grp.Memories)
                .Load();

            return mgroup.Memories.ToList().Select(m => MemoriesService.ConvertToMeomoryVM(m)).ToList();
        }

        public async Task<List<Group_User>> GetMgroupUsers(int id)
        {
            var mgroup = await _context.Groups.FindAsync(id);

            _context.Entry(mgroup)
                .Collection(grp => grp.Groups_Users)
                .Query()
                .Include(gu => gu.User)
                .Load();

            return mgroup.Groups_Users.ToList();
        }

        public async Task PutMgroup(GroupUpdateVM group)
        {
            var mGroup = ConvertToGroupFromUpdate(group);
            _context.Entry(mGroup).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task PostMgroup(GroupVM mgroup)
        {
            var mGroup = ConvertToGroup(mgroup);
            _context.Groups.Add(mGroup); 

            await _context.SaveChangesAsync();
        }

        public async Task PostMgroupUser(Group_User groupUser)
        {
            //var mGroup = ConvertToGroup(mgroup);
            _context.Groups_Users.Add(groupUser);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMgroup(int id)
        {
            var mgroup = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(mgroup);

            await _context.SaveChangesAsync();
        }

        public bool MgroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }

        public bool MgroupUserExists(int groupId, int userId)
        {
            return _context.Groups_Users.Any(e => e.GroupId == groupId && e.UserId == userId);
        }

        private static GroupVM ConvertToMgroupVM(Group mgroup)
        {
            if (mgroup == null)
            {
                return new GroupVM();
            }

            return new GroupVM()
            {
                Name = mgroup.Name,
            };
        }

        private static GroupMemoryVM ConvertToMgroupMemoryVM(Group mgroup)
        {
            if (mgroup == null)
            {
                return new GroupMemoryVM();
            }

            var memories = mgroup.Memories.ToList();
            var memoriesVM = memories.Select(m => MemoriesService.ConvertToMeomoryVM(m)).ToList();

            return new GroupMemoryVM()
            {
                Id = mgroup.Id,
                Name = mgroup.Name,
                Memories = memoriesVM
            };
        }

        private static Group ConvertToGroup(GroupVM mgroup)
        {
            return new Group()
            {
                Name = mgroup.Name
            };
        }

        private static Group ConvertToGroupFromUpdate(GroupUpdateVM group)
        {
            return new Group()
            {
                Id = group.Id,
                Name = group.Name
            };
        }
    }
}

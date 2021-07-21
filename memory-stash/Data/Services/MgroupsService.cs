using memory_stash.Data.ViewModels;
using memory_stash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<MgroupVM> GetMgroup(int id)
        {
            var mgroup = await _context.Mgroups.FindAsync(id);

            return ConvertToMgroupVM(mgroup);
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
                Name = mgroup.Name
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

using memory_stash.Data.Models;
using memory_stash.Data.ViewModels;
using memory_stash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memory_stash.Data.Services
{
    public class MusersService
    {
        private readonly MemoryStashDbContext _context;

        public MusersService(MemoryStashDbContext context)
        {
            _context = context;
        }

        public async Task<List<MuserVM>> GetMusers()
        {
            return await _context.Musers
                .Select(u => ConvertToMuserVM(u))
                .ToListAsync();
        }

        public async Task<MuserVM> GetMuser(int id)
        {
            var muser = await _context.Musers.FindAsync(id);

            return ConvertToMuserVM(muser);
        }

        public async Task<List<GroupUser>> GetUsersGroup(int id)
        {
            var muser = await _context.Musers.FindAsync(id);

            _context.Entry(muser)
                .Collection(usr => usr.GroupUsers)
                .Query()
                .Include(gu => gu.Group)
                .Load();

            return muser.GroupUsers.ToList();
        }

        public async Task PutMuser(MuserVM muserVM)
        {
            var muser = ConvertToMuser(muserVM);
            _context.Entry(muser).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task PostMuser(MuserVM muserVM)
        {
            var muser = ConvertToMuser(muserVM);
            _context.Musers.Add(muser);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMuser(int id)
        {
            var muser = await _context.Musers.FindAsync(id);
            _context.Musers.Remove(muser);

            await _context.SaveChangesAsync();
        }

        public bool MuserExists(int id)
        {
            return _context.Musers.Any(e => e.Id == id);
        }

        public static MuserVM ConvertToMuserVM(Muser user)
        {
            if (user == null)
            {
                return new MuserVM();
            }

            return new MuserVM()
            {
                Id = user.Id,
                Name= user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }

        public static Muser ConvertToMuser(MuserVM user)
        {
            return new Muser()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}

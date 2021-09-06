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
    public class UsersService
    {
        private readonly MemoryStashDbContext _context;

        public UsersService(MemoryStashDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserVM>> GetMusers()
        {
            return await _context.Users
                .Select(u => ConvertToMuserVM(u))
                .ToListAsync();
        }

        public async Task<UserVM> GetMuser(int id)
        {
            var muser = await _context.Users.FindAsync(id);

            return ConvertToMuserVM(muser);
        }

        public async Task<List<Group_User>> GetUsersGroup(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Entry(user)
                .Collection(usr => usr.Groups_Users)
                .Query()
                .Include(gu => gu.Group)
                .Load();

            return user.Groups_Users.ToList();
        }

        public async Task PutMuser(UserVM muserVM)
        {
            var user = ConvertToMuser(muserVM);
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task PostMuser(UserVM muserVM)
        {
            var user = ConvertToMuser(muserVM);
            _context.Users.Add(user);

            _context.Database.OpenConnection();
            try
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
                await _context.SaveChangesAsync();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            //await _context.SaveChangesAsync();
        }

        public async Task DeleteMuser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public bool MuserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public static UserVM ConvertToMuserVM(User user)
        {
            if (user == null)
            {
                return new UserVM();
            }

            return new UserVM()
            {
                Id = user.Id,
                Name= user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }

        public static User ConvertToMuser(UserVM user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}

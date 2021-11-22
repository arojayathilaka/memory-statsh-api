using memory_stash.Data.ViewModels;
using memory_stash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using memory_stash.Data.Models;

namespace memory_stash.Data.Services
{
    public class MemoriesService
    {
        private readonly MemoryStashDbContext _context;

        public MemoriesService(MemoryStashDbContext context)
        {
            _context = context;
        }

        public async Task<List<MemoryVM>> GetMemories()
        {
            return await _context.Memories
                .Select(m => ConvertToMeomoryVM(m))
                .ToListAsync();
        }

        public async Task<List<MemoryGroupVM>> GetMemoriesWithGroup()
        {
            var memories = await _context.Memories
                .Include(m => m.Group)
                .Select(m => ConvertToMemoryGroupVM(m)).ToListAsync();

            return memories;
        }

        public async Task PutMemory(MemoryUpdateVM memoryVM)
        {
            var memory = ConvertToMemoryFromUpdate(memoryVM);
            _context.Entry(memory).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


        public async Task PostMemory(MemoryAddVM memoryVM)
        {
            var memory = ConvertToMemoryFromAdd(memoryVM);
            _context.Memories.Add(memory);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteMemory(int id)
        {
            var memory = await _context.Memories.FindAsync(id);
            _context.Memories.Remove(memory);

            await _context.SaveChangesAsync();
        }

        public bool MemoryExists(int id)
        {
            return _context.Memories.Any(e => e.Id == id);
        }

        public async Task<Memory> GetMemory(int id)
        {
            var mgroup = await _context.Memories.FindAsync(id);

            return mgroup;
        }

        public static MemoryVM ConvertToMeomoryVM(Memory memory)
        {
            if (memory == null)
            {
                return new MemoryVM();
            }

            return new MemoryVM()
            {
                Id= memory.Id,
                Mdate = memory.Mdate,
                Title = memory.Title,
                Description = memory.Description,
                GroupId = memory.GroupId,
            };
        }

        public static Memory ConvertToMemory(MemoryVM memory)
        {
            return new Memory()
            {
                Mdate = memory.Mdate,
                Title = memory.Title,
                Description = memory.Description,
                GroupId = memory.GroupId,
            };
        }

        private static Memory ConvertToMemoryFromUpdate(MemoryUpdateVM memory)
        {
            return new Memory()
            {
                Id = memory.Id,
                Mdate = memory.Mdate,
                Title = memory.Title,
                Description = memory.Description,
                GroupId = memory.GroupId,
            };
        }

        private static Memory ConvertToMemoryFromAdd(MemoryAddVM memory)
        {
            return new Memory()
            {
                Mdate = memory.Mdate,
                Title = memory.Title,
                Description = memory.Description,
                GroupId = memory.GroupId,
            };
        }

        private static MemoryGroupVM ConvertToMemoryGroupVM(Memory memory)
        {
            if (memory == null)
            {
                return new MemoryGroupVM();
            }

            var group = memory.Group;
            var groupVM = new GroupVM();
            if (group != null)
            {
               groupVM = GroupsService.ConvertToMgroupVM(group);           
            } 
            

            return new MemoryGroupVM()
            {
                Id = memory.Id,
                Mdate = memory.Mdate,
                Title = memory.Title,
                Description = memory.Description,
                GroupId = memory.GroupId,
                Group = groupVM
            };
        }
    }
}

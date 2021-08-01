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

        public async Task PutMemory(MemoryVM memoryVM)
        {
            var memory = ConvertToMemory(memoryVM);
            _context.Entry(memory).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task PostMemory(MemoryVM memoryVM)
        {
            var memory = ConvertToMemory(memoryVM);
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

        public async Task<MemoryVM> GetMemory(int id)
        {
            var mgroup = await _context.Memories.FindAsync(id);

            return ConvertToMeomoryVM(mgroup);
        }

        public static MemoryVM ConvertToMeomoryVM(Memory memory)
        {
            if (memory == null)
            {
                return new MemoryVM();
            }

            return new MemoryVM()
            {
                Id = memory.Id,
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
                Id = memory.Id,
                Mdate = memory.Mdate,
                Title = memory.Title,
                Description = memory.Description,
                GroupId = memory.GroupId,
            };
        }
    }
}

using memory_stash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memory_stash.Data.ViewModels
{
    public class GroupMemoryVM
    {
        //public MgroupMemoryVM()
        //{
        //    Memories = new HashSet<Memory>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public List<MemoryVM> Memories { get; set; }
    }
}


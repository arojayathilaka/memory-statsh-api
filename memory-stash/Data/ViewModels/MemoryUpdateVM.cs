using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memory_stash.Data.ViewModels
{
    public class MemoryUpdateVM
    {
        public int Id { get; set; }
        public DateTime? Mdate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }

    }
}

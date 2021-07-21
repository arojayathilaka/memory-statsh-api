using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Models
{
    public partial class Memory
    {
        public Memory()
        {
            MemoryImages = new HashSet<MemoryImage>();
        }

        public int Id { get; set; }
        public DateTime? Mdate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        // Navigation properties
        public int GroupId { get; set; }
        public virtual Mgroup Group { get; set; }
        public virtual ICollection<MemoryImage> MemoryImages { get; set; }
    }
}

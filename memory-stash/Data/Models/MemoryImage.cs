using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Models
{
    public partial class MemoryImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }


        // Navigation properties
        public int MemoryId { get; set; }
        public virtual Memory Memory { get; set; }
    }
}

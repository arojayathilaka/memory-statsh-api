using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Data.Models
{
    public partial class Friend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Bdate { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual List<FriendImage> FriendImages { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Models
{
    public partial class Friend
    {
        public Friend()
        {
            FriendImages = new HashSet<FriendImage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Bdate { get; set; }
        public int UserId { get; set; }

        public virtual Muser User { get; set; }
        public virtual ICollection<FriendImage> FriendImages { get; set; }
    }
}

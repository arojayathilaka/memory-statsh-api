using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Models
{
    public partial class Muser
    {
        public Muser()
        {
            Friends = new HashSet<Friend>();
            GroupUsers = new HashSet<GroupUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}

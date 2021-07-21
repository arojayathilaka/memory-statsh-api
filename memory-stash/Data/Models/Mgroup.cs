using memory_stash.Data.ViewModels;
using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Models
{
    public partial class Mgroup
    {
        public Mgroup()
        {
            GroupUsers = new HashSet<GroupUser>();
            Memories = new HashSet<Memory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<Memory> Memories { get; set; }
    }
}

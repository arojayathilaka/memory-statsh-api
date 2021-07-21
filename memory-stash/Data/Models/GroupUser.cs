using System;
using System.Collections.Generic;

#nullable disable

namespace memory_stash.Models
{
    public partial class GroupUser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }

        public virtual Mgroup Group { get; set; }
        public virtual Muser User { get; set; }
    }
}

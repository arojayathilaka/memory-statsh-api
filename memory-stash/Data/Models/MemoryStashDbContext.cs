using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace memory_stash.Data.Models
{
    public partial class MemoryStashDbContext : DbContext
    {
        public MemoryStashDbContext()
        {
        }

        public MemoryStashDbContext(DbContextOptions<MemoryStashDbContext> options)
            : base(options)
        {
        }
            
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<FriendImage> FriendImages { get; set; }
        public virtual DbSet<Group_User> Groups_Users { get; set; }
        public virtual DbSet<Memory> Memories { get; set; }
        public virtual DbSet<MemoryImage> MemoryImages { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group_User>()
                .HasOne(gu => gu.Group)
                .WithMany(gu => gu.Groups_Users)
                .HasForeignKey(gu => gu.GroupId);

            modelBuilder.Entity<Group_User>()
                .HasOne(gu => gu.User)
                .WithMany(gu => gu.Groups_Users)
                .HasForeignKey(gu => gu.UserId);
        }
    }
}

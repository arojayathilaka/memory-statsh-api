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
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<Memory> Memories { get; set; }
        public virtual DbSet<MemoryImage> MemoryImages { get; set; }
        public virtual DbSet<Mgroup> Mgroups { get; set; }
        public virtual DbSet<Muser> Musers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.ToTable("Friend");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Bdate)
                    .HasColumnType("date")
                    .HasColumnName("BDate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Friends)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friend");
            });

            modelBuilder.Entity<FriendImage>(entity =>
            {
                entity.ToTable("FriendImage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.FriendImages)
                    .HasForeignKey(d => d.FriendId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FriendImage");
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("GroupUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupUser1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupUser2");
            });

            modelBuilder.Entity<Memory>(entity =>
            {
                entity.ToTable("Memory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mdate)
                    .HasColumnType("date")
                    .HasColumnName("MDate");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Memories)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMemory");
            });

            modelBuilder.Entity<MemoryImage>(entity =>
            {
                entity.ToTable("MemoryImage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Memory)
                    .WithMany(p => p.MemoryImages)
                    .HasForeignKey(d => d.MemoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemoryImage");
            });

            modelBuilder.Entity<Mgroup>(entity =>
            {
                entity.ToTable("MGroup");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Muser>(entity =>
            {
                entity.ToTable("MUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

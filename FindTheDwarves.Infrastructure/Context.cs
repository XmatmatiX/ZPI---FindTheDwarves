using FindTheDwarves.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Infrastructure
{
    public class Context : DbContext
    {

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Dwarf> Dwarves { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AchievementDwarf>()
                .HasKey(ad => new { ad.DwarfID, ad.AchievementID });

            modelBuilder.Entity<AchievementDwarf>()
                .HasOne(ad => ad.Dwarf)
                .WithMany(d => d.Achivements)
                .HasForeignKey(ad => ad.DwarfID);

            modelBuilder.Entity<AchievementDwarf>()
                .HasOne(ad => ad.Achievement)
                .WithMany(a => a.Dwarves)
                .HasForeignKey(ad => ad.AchievementID);


            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.UserID, ua.AchievementID });

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.Achivements)
                .HasForeignKey(ua => ua.UserID);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.Achievement)
                .WithMany(a => a.Users)
                .HasForeignKey(ua => ua.AchievementID);


            modelBuilder.Entity<UserDwarf>()
                .HasKey(ud => new { ud.UserID, ud.DwarfID });

            modelBuilder.Entity<UserDwarf>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.Dwarves)
                .HasForeignKey(ud => ud.UserID);

            modelBuilder.Entity<UserDwarf>()
                .HasOne(ud => ud.Dwarf)
                .WithMany(d => d.Users)
                .HasForeignKey(ud => ud.DwarfID);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prose.Models;

namespace Prose.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<ClubUser> ClubUser { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Vote> Vote { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Club>().HasData(
               new Club()
               {
                   ClubId = 1,
                   Name = "Bookish Broads",
                   Location = "Nashville, TN",
                   Description = "A relaxed group of ladies who all know Asia somehow.",
                   MeetingFrequency = "Once a month",
                   UserId = user.Id
               },
               new Club()
               {
                   ClubId = 2,
                   Name = "Kingers",
                   Location = "Nashville, TN",
                   Description = "Stephen King themed club.",
                   MeetingFrequency = "Once bimonthly",
                   UserId = user.Id
               },
               new Club()
               {
                   ClubId = 3,
                   Name = "SJ Readers of Nashville",
                   Location = "Nashville, TN",
                   Description = "A social justice oriented book club for all Nashvillians",
                   MeetingFrequency = "Twice a month",
                   UserId = user.Id
               }
           );
            modelBuilder.Entity<ClubUser>().HasData(
               new ClubUser()
               {
                   ClubUserId = 1,
                   ClubId = 1,
                   UserId = user.Id
               },
               new ClubUser()
               {
                   ClubUserId = 2,
                   ClubId = 2,
                   UserId = user.Id
               },
               new ClubUser()
               {
                   ClubUserId = 3,
                   ClubId = 3,
                   UserId = user.Id
               }
           );
            modelBuilder.Entity<Vote>().HasData(
               new Vote()
               {
                   VoteId = 1,
                   ClubUserId = 1,
                   BookId = 2
               },
               new Vote()
               {
                   VoteId = 2,
                   ClubUserId = 2,
                   BookId = 2
               },
               new Vote()
               {
                   VoteId = 3,
                   ClubUserId = 3,
                   BookId = 3
               }
           );
            modelBuilder.Entity<Book>().HasData(
               new Book()
               {
                   BookId = 1,
                   Title = "Pachinko",
                   Author = "Min Jin Lee",
                   Details = "A riveting tale about something",
                   ClubUserId = 1,
                   CurrentlyReading = true,
                   PastRead = false
               },
               new Book()
               {
                   BookId = 2,
                   Title = "Sugar",
                   Author = "Baby Spice",
                   Details = "An autobiographical look into the life of the sweetest member of the Spice Girls",
                   ClubUserId = 2,
                   CurrentlyReading = false,
                   PastRead = false
               },
               new Book()
               {
                   BookId = 3,
                   Title = "George Foreman: Life and Tales",
                   Author = "George Foreman",
                   Details = "Blah blah",
                   ClubUserId = 1,
                   CurrentlyReading = false,
                   PastRead = false
               }
            );
        }
    }
}

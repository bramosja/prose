﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prose.Data;

namespace Prose.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Prose.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<int>("ClubUserId");

                    b.Property<bool>("CurrentlyReading");

                    b.Property<string>("Details");

                    b.Property<int>("ISBN");

                    b.Property<string>("Image");

                    b.Property<bool>("PastRead");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("BookId");

                    b.HasIndex("ClubUserId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "Min Jin Lee",
                            ClubUserId = 1,
                            CurrentlyReading = true,
                            Details = "A riveting tale about something",
                            ISBN = 0,
                            PastRead = false,
                            Title = "Pachinko"
                        },
                        new
                        {
                            BookId = 2,
                            Author = "Baby Spice",
                            ClubUserId = 2,
                            CurrentlyReading = false,
                            Details = "An autobiographical look into the life of the sweetest member of the Spice Girls",
                            ISBN = 0,
                            PastRead = false,
                            Title = "Sugar"
                        },
                        new
                        {
                            BookId = 3,
                            Author = "George Foreman",
                            ClubUserId = 1,
                            CurrentlyReading = false,
                            Details = "Blah blah",
                            ISBN = 0,
                            PastRead = false,
                            Title = "George Foreman: Life and Tales"
                        });
                });

            modelBuilder.Entity("Prose.Models.Club", b =>
                {
                    b.Property<int>("ClubId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("MeetingFrequency");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("ClubId");

                    b.ToTable("Club");

                    b.HasData(
                        new
                        {
                            ClubId = 1,
                            Description = "A relaxed group of ladies who all know Asia somehow.",
                            Location = "Nashville, TN",
                            MeetingFrequency = "Once a month",
                            Name = "Bookish Broads",
                            UserId = "5dc2e3d2-5218-4438-9e9c-642cf35b88db"
                        },
                        new
                        {
                            ClubId = 2,
                            Description = "Stephen King themed club.",
                            Location = "Nashville, TN",
                            MeetingFrequency = "Once bimonthly",
                            Name = "Kingers",
                            UserId = "5dc2e3d2-5218-4438-9e9c-642cf35b88db"
                        },
                        new
                        {
                            ClubId = 3,
                            Description = "A social justice oriented book club for all Nashvillians",
                            Location = "Nashville, TN",
                            MeetingFrequency = "Twice a month",
                            Name = "SJ Readers of Nashville",
                            UserId = "5dc2e3d2-5218-4438-9e9c-642cf35b88db"
                        });
                });

            modelBuilder.Entity("Prose.Models.ClubUser", b =>
                {
                    b.Property<int>("ClubUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClubId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("ClubUserId");

                    b.HasIndex("ClubId");

                    b.HasIndex("UserId");

                    b.ToTable("ClubUser");

                    b.HasData(
                        new
                        {
                            ClubUserId = 1,
                            ClubId = 1,
                            UserId = "5dc2e3d2-5218-4438-9e9c-642cf35b88db"
                        },
                        new
                        {
                            ClubUserId = 2,
                            ClubId = 2,
                            UserId = "5dc2e3d2-5218-4438-9e9c-642cf35b88db"
                        },
                        new
                        {
                            ClubUserId = 3,
                            ClubId = 3,
                            UserId = "5dc2e3d2-5218-4438-9e9c-642cf35b88db"
                        });
                });

            modelBuilder.Entity("Prose.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<int>("ClubUserId");

                    b.HasKey("VoteId");

                    b.ToTable("Vote");

                    b.HasData(
                        new
                        {
                            VoteId = 1,
                            BookId = 2,
                            ClubUserId = 1
                        },
                        new
                        {
                            VoteId = 2,
                            BookId = 2,
                            ClubUserId = 2
                        },
                        new
                        {
                            VoteId = 3,
                            BookId = 3,
                            ClubUserId = 3
                        });
                });

            modelBuilder.Entity("Prose.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "5dc2e3d2-5218-4438-9e9c-642cf35b88db",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "154c5e54-12dc-41a7-8d4f-33039dd8e08b",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDTHpO8TNreq4LOWX97Z7kS2LAfo2UxkGGUN9Yb1DpGjeuLUur39GNk3azl9i/wiww==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fd274816-2fd0-49da-aa19-c439110f384a",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com",
                            FirstName = "admin",
                            LastName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Prose.Models.Book", b =>
                {
                    b.HasOne("Prose.Models.ClubUser", "ClubUser")
                        .WithMany()
                        .HasForeignKey("ClubUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Prose.Models.ClubUser", b =>
                {
                    b.HasOne("Prose.Models.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Prose.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

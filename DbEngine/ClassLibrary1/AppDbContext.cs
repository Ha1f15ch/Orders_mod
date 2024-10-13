using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=UsersOrders;Username=postgres;Password=Pass1!2@");
        }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        }

        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasData(
                    new User
                    {
                        Id = 1,
                        Name = "admin",
                        Email = "alexy.alexy98@mail.ru",
                        Password = "admin",
                        PhoneNumber = "79518306637",
                    },
                    new User
                    {
                        Id = 2,
                        Name = "user",
                        Email = "alex.alexy98@mail.ru",
                        Password = "user",
                        PhoneNumber = "88005553535"
                    }
                );
            }
        }

        public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
        {
            public void Configure(EntityTypeBuilder<UserProfile> builder)
            {
                builder.HasData(
                    new UserProfile
                    {
                        Id = 1,
                        FirstName = "Alexey",
                        MiddleName = "Dmitrievich",
                        LastName = "Franchuk",
                        Birthday = new DateTime(1998, 10, 27, 0, 0, 0, DateTimeKind.Utc),
                        IsActived = true,
                        DateCreatedAt = DateTime.UtcNow,
                        DateUpdatedAt = DateTime.UtcNow,
                        DateDelete = null,
                        UserId = 1,
                    },
                    new UserProfile
                    {
                        Id = 2,
                        FirstName = "Igor",
                        MiddleName = "Vasilievich",
                        LastName = "Menschin",
                        Birthday = new DateTime(1970, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                        IsActived = true,
                        DateCreatedAt = DateTime.UtcNow,
                        DateUpdatedAt = DateTime.UtcNow,
                        DateDelete = null,
                        UserId = 2,
                    }
                );

                builder.Property(y => y.DateCreatedAt).HasConversion(
                    t => t.ToUniversalTime(), 
                    t => DateTime.SpecifyKind(t, DateTimeKind.Utc)
                );
                builder.Property(y => y.DateUpdatedAt).HasConversion(
                    t => t.ToUniversalTime(),
                    t => DateTime.SpecifyKind(t, DateTimeKind.Utc)
                );
                builder.Property(y => y.DateDelete).HasConversion(
                    t => t.HasValue ? t.Value.ToUniversalTime() : (DateTime?)null,
                    t => t.HasValue ? DateTime.SpecifyKind(t.Value, DateTimeKind.Utc) : (DateTime?)null
                );
            }
        }

        public class RoleConfiguration : IEntityTypeConfiguration<Role>
        {
            public void Configure(EntityTypeBuilder<Role> builder)
            {
                Role user = new Role { Id = 1, RoleName = "USER" };
                Role admin = new Role { Id = 2, RoleName = "ADMIN" };
                Role moderator = new Role { Id = 3, RoleName = "MODERATOR" };
                Role guest = new Role { Id = 4, RoleName = "GUEST" };

                builder.HasData(user, admin, moderator, guest);
            }
        }

        public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
        {
            public void Configure(EntityTypeBuilder<UserRole> builder)
            {
                builder.HasData(
                    new UserRole { RoleId = 1, UserId = 1 },
                    new UserRole { RoleId = 2, UserId = 1},
                    new UserRole { RoleId = 3, UserId = 1},
                    new UserRole { RoleId = 4, UserId = 1},
                    new UserRole { RoleId = 1, UserId = 2},
                    new UserRole { RoleId = 4, UserId = 2}
                );
                
            }
        }


    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext.EntityConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole { Id = 1, RoleId = 1, UserId = 1 },
                new UserRole { Id = 2, RoleId = 2, UserId = 1 },
                new UserRole { Id = 3, RoleId = 3, UserId = 1 },
                new UserRole { Id = 4, RoleId = 4, UserId = 1 },
                new UserRole { Id = 5, RoleId = 1, UserId = 2 },
                new UserRole { Id = 6, RoleId = 4, UserId = 2 }
            );

        }
    }
}

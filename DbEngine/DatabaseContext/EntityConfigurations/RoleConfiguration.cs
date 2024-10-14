using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseContext.EntityConfigurations
{
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
}

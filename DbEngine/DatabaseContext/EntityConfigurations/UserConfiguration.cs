using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseContext.EntityConfigurations
{
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
}

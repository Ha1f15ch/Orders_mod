using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseContext.EntityConfigurations
{
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
}

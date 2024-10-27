using LogServices;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.Extensions.Logging;

namespace DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public DbSet<OrderPriority> OrderPriorities { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderScores> OrderScores { get; set; } = null!;
        public DbSet<AssignersRequests> AssignersRequests { get; set; } = null!;
        public DbSet<RequestsToCancellation> RequestsToCancellations { get; set; } = null!;
        public DbSet<Token> Tokens { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(SharedLoggerProvider);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityConfigurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.RoleConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.OrderPriorityConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.OrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.OrderConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.OrderScoresConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.AssignersRequestsConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.RequestsToCancellationConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.TokenConfiguration());
        }

        public static readonly ILoggerFactory SharedLoggerProvider = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new SharedLoggerProvider());
        });
    }
}

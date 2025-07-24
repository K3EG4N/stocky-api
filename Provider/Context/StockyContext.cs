using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Provider.Configuration;

namespace Provider.Context
{
    public class StockyContext : DbContext
    {
        public StockyContext(DbContextOptions<StockyContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            
            modelBuilder.Entity<Role>().ToTable("Roles", "usr");
            modelBuilder.Entity<User>().ToTable("Users", "usr");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", "usr");
            modelBuilder.Entity<Person>().ToTable("Person", "per");
            modelBuilder.Entity<PersonType>().ToTable("PersonTypes", "per");
        }
    }
}

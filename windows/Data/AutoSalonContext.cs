using Microsoft.EntityFrameworkCore;
using AutoSalon.Desktop.Data.Models;

namespace AutoSalon.Desktop.Data
{
    public class AutoSalonContext : DbContext
    {
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;" +
            "Database=AutoSalonDB;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";

        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Индексы/уникальность
            modelBuilder.Entity<Manufacturer>()
                .HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Brand>()
                .HasIndex(x => new { x.ManufacturerId, x.Name }).IsUnique();

            modelBuilder.Entity<Car>()
                .HasIndex(x => x.VIN).IsUnique();

            // Цена
            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Car>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Car_Price", "[Price] > 0");
                    t.HasCheckConstraint("CK_Car_VINLen", "LEN([VIN]) = 17");
                });

            // Default timestamps на стороне SQL Server (важно!)
            modelBuilder.Entity<Manufacturer>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");
            modelBuilder.Entity<Brand>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");
            modelBuilder.Entity<Car>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");
            modelBuilder.Entity<User>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            // Seed — ТОЛЬКО СТАТИКА (никаких DateTime.UtcNow / Guid.NewGuid / Hash() в рантайме)
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "Viewer" }
            );

            // SHA256("admin") = 8C6976E5...
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Login = "admin",
                    PasswordHash = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918",
                    RoleId = 1,
                    IsActive = true
                }
            );
        }
    }
}

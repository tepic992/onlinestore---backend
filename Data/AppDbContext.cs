using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Postojeće relacije
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            // Mapiranje tabela na PostgreSQL imena sa velikim početnim slovom
            modelBuilder.Entity<User>().ToTable("\"Users\"");
            modelBuilder.Entity<Category>().ToTable("\"Categories\"");
            modelBuilder.Entity<Product>().ToTable("\"Products\"");
            modelBuilder.Entity<Order>().ToTable("\"Orders\"");
            modelBuilder.Entity<OrderItem>().ToTable("\"OrderItems\"");
            modelBuilder.Entity<CartItem>().ToTable("\"CartItems\"");
            modelBuilder.Entity<Address>().ToTable("\"Addresses\"");
            modelBuilder.Entity<Review>().ToTable("\"Reviews\"");
            modelBuilder.Entity<Payment>().ToTable("\"Payments\"");
            modelBuilder.Entity<AuditLog>().ToTable("\"AuditLogs\"");
            modelBuilder.Entity<Discount>().ToTable("\"Discounts\"");
            modelBuilder.Entity<ProductImage>().ToTable("\"ProductImages\"");
            modelBuilder.Entity<Brand>().ToTable("\"Brands\"");

            base.OnModelCreating(modelBuilder);
        }

    }
}

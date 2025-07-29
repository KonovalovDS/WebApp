using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;

public class AppDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    private readonly ILogger<AppDbContext> _logger;

    public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options) {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        _logger.LogInformation("Инициализация модели данных");
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Order>()
            .OwnsOne(o => o.Details, details => {
                details.OwnsOne(d => d.Customer);
                details.OwnsOne(d => d.ShippingAddress);
                details.OwnsMany(d => d.Items, items => {
                    items.WithOwner().HasForeignKey("OrderId");
                    items.HasKey("Id");
                });
            });
    }
}

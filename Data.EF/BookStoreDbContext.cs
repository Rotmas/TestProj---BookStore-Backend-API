using Microsoft.EntityFrameworkCore;

namespace Data.EF;

public class BookStoreDbContext : DbContext
{
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderBookEntity> OrderBooks { get; set; }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderBookEntity>()
            .HasKey(ob => new { ob.OrderId, ob.BookId });

        modelBuilder.Entity<OrderBookEntity>()
            .HasOne(ob => ob.Order)
            .WithMany(o => o.OrderBooks)
            .HasForeignKey(ob => ob.OrderId);

        modelBuilder.Entity<OrderBookEntity>()
            .HasOne(ob => ob.Book)
            .WithMany(b => b.OrderBooks)
            .HasForeignKey(ob => ob.BookId);
    }
}

using Microsoft.EntityFrameworkCore;
using hip_hop.Models;

public class Hip_hopDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<OrderType> OrderTypes { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order {Id = 1, Name = "John Gray", Status = true, Phone = "789-456-3456", Email = "JohnGray@aol.com", OrderTypeId = 2, DateClosed = null, PaymentTypeId = null, Tip = 0M },
            new Order {Id = 2, Name = "Jenna Blue", Status = false, Phone = "383-308-1093", Email = "Jenna@yahoo.com", OrderTypeId = 1, DateClosed = new DateTime(2024, 03, 29), PaymentTypeId = 1, Tip = 7.50M },
            new Order {Id = 3, Name = "Sarah Red", Status = true, Phone = "883-488-2239", Email = "SRed@hotmail.com", OrderTypeId = 1, DateClosed = null, PaymentTypeId = null, Tip = 0M },
            new Order {Id = 4, Name = "Charles Peach", Status = false, Phone = "112-456-3947", Email = "Peach@peach.com", OrderTypeId = 1, DateClosed = new DateTime(2024, 04, 02), PaymentTypeId = 2, Tip = 10.00M }
        });

        modelBuilder.Entity<Item>().HasData(new Item[]
        {
            new Item {Id = 1, OrderItem = "Sausage Pepperoni Pizza", ItemPrice = 18.99M},
            new Item {Id = 2, OrderItem = "Spicy Wings", ItemPrice = 12.50M},
            new Item {Id = 3, OrderItem = "Hawaiian Pizza", ItemPrice = 15.99M},
            new Item {Id = 4, OrderItem = "The Big Greek Pizza", ItemPrice = 21.99M},
            new Item {Id = 5, OrderItem = "Honey Glazed Wings", ItemPrice = 14.99M},
            new Item {Id = 6, OrderItem = "Cheesy Breadsticks", ItemPrice = 9.99M},
            new Item {Id = 7, OrderItem = "So Much Cheese Pizza", ItemPrice = 18.99M},
            new Item {Id = 8, OrderItem = "The Buffalo Wing Platter", ItemPrice = 24.99M},
            new Item {Id = 9, OrderItem = "The Spicy Meaty Pizza", ItemPrice = 22.99M}
        });

        modelBuilder.Entity<OrderType>().HasData(new OrderType[]
        {
            new OrderType {Id = 1, Type = "Walk-In"},
            new OrderType {Id = 2, Type = "Call-In"}
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType {Id = 1, Type = "Cash"},
            new PaymentType {Id = 2, Type = "Card"},
            new PaymentType {Id = 3, Type = "Check"}
        });

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User {Id = 1, Name = "Joey Ebach", Email = "Connect@JoeyEbach.com", Uid = null}
        });
    }

    public Hip_hopDbContext(DbContextOptions<Hip_hopDbContext> context) : base(context)
    {

    }
}



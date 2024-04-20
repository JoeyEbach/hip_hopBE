using hip_hop.Models;
using hip_hop.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddNpgsql<Hip_hopDbContext>(builder.Configuration["Hip_hopDbConnectionString"]);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();


// ***Check User
app.MapPost("/checkuser/{uid}", (Hip_hopDbContext db, string uid, UserDto dto) =>
{
    User thisUser = db.Users.FirstOrDefault(u => u.Uid == uid);

    if (thisUser == null) 
    {
        try
        {
            User newUser = new() { Uid = dto.Uid };
            db.Users.Add(newUser);
            db.SaveChanges();
            return Results.Ok(thisUser);
        }
        catch
        {
            return Results.BadRequest();
        }
    }

    return Results.Ok(thisUser);
});

// ***Get All Orders
app.MapGet("/orders", (Hip_hopDbContext db) =>
{
    return db.Orders
            .Include(o => o.OrderType)
            .Include(o => o.PaymentType)
            .Select(o => new
            {
                o.Id,
                o.Name,
                o.Phone,
                o.Email,
                OrderType = o.OrderType.Type,
                Status = o.Status ? "Open" : "Closed"
            })
            .ToList();
});

// ***Get All Items
app.MapGet("/items", (Hip_hopDbContext db) =>
{
    return db.Items
            .Select(i => new
            {
                i.Id,
                i.OrderItem,
                i.ItemPrice
            })
            .ToList();
});

// ***Create A New Order
app.MapPost("/orders/new", (Hip_hopDbContext db, OrderDto dto) =>
{
    Order newOrder = new() { Name = dto.Name, Status = true, Email = dto.Email, Phone = dto.Phone, OrderTypeId = dto.OrderTypeId };
    try
    {
        db.Orders.Add(newOrder);
        db.SaveChanges();
        return Results.Created($"/orders/new/{newOrder.Id}", newOrder);
    }
    catch
    {
        return Results.BadRequest();
    }
});

// ***Get All Closed Orders
app.MapGet("/orders/closed", (Hip_hopDbContext db) =>
{
    return db.Orders
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Item)
            .Include(o => o.OrderType)
            .Include(o => o.PaymentType)
            .Where(o => o.Status == false)
            .Select(o => new
            {
                o.Id,
                o.Name,
                o.PaymentType.Type,
                o.DateClosed,
                o.OrderTotal,
                o.TotalAndTip
            })
            .ToList();
});

// ***Add Item To Order
app.MapPost("/orders/{orderId}/add/{itemId}", (Hip_hopDbContext db, int orderId, int itemId) =>
{
    Order thisOrder = db.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == orderId);
    Item thisItem = db.Items.FirstOrDefault(i => i.Id == itemId);

    if (thisOrder == null || thisItem == null)
    {
        return Results.NotFound();
    }

    OrderItem newOrderItem = new() { Item = thisItem, Order = thisOrder };
    db.OrderItems.Add(newOrderItem);
    db.SaveChanges();
    return Results.Ok();
});

// ***Remove Item From Order
app.MapDelete("/orders/{orderId}/remove/{itemId}", (Hip_hopDbContext db, int orderId, int itemId) =>
{

    OrderItem thisOrderItem = db.OrderItems
                        .Where(oi => oi.Order.Id == orderId)
                        .Where(oi => oi.Item.Id == itemId)
                        .FirstOrDefault();

    if (thisOrderItem == null)
    {
        return Results.NotFound();
    }

    db.OrderItems.Remove(thisOrderItem);
    db.SaveChanges();
    return Results.NoContent();
});

// ***Get Single Item
app.MapGet("/items/{itemId}", (Hip_hopDbContext db, int itemId) =>
{
    return db.Items
            .Where(i => i.Id == itemId)
            .Select(i => new
            {
                i.Id,
                i.OrderItem,
                i.ItemPrice
            })
            .SingleOrDefault();           
});

// ***Get Single Order
app.MapGet("/orders/{orderId}", (Hip_hopDbContext db, int orderId) =>
{
    return db.Orders
            .Include(o => o.OrderType)
            .Include(o => o.PaymentType)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Item)
            .Where(o => o.Id == orderId)
            .Select(o => new
            {
                o.Id,
                o.Name,
                o.Phone,
                o.Email,
                OrderType = o.OrderType.Type,
                PaymentType = o.PaymentTypeId.HasValue ? o.PaymentType.Type : null,
                o.Tip,
                Status = o.Status ? "Open" : "Closed",
                Total = o.OrderTotal,
                TotalPlusTip = o.TotalAndTip,
                DateClosed = o.DateClosed != null ? o.DateClosed.Value.ToString("MM/dd/yyyy") : null,
                Items = o.Items.Select(i => new
                {
                    i.Item.Id,
                    i.Item.OrderItem,
                    i.Item.ItemPrice
                })
            })
            .SingleOrDefault();
});

// ***Delete Order
app.MapDelete("/orders/{orderId}/delete", (Hip_hopDbContext db, int orderId) =>
{
    Order thisOrder = db.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);
    if (thisOrder == null)
    {
        return Results.NotFound();
    }

    db.Orders.Remove(thisOrder);
    db.SaveChanges();
    return Results.NoContent();
});

// ***Update Order
app.MapPut("/orders/{orderId}/update", (Hip_hopDbContext db, int orderId, OrderDto dto) =>
{
    Order thisOrder = db.Orders.FirstOrDefault(o => o.Id == orderId);
    if (thisOrder == null)
    {
        return Results.NotFound();
    }
    thisOrder.Name = dto.Name;
    thisOrder.Email = dto.Email;
    thisOrder.Phone = dto.Phone;
    thisOrder.OrderTypeId = dto.OrderTypeId;
    db.SaveChanges();
    return Results.Ok();
});

// ***Close Order
app.MapPut("/order/close", (Hip_hopDbContext db, CloseOrderDto dto) =>
{
    Order thisOrder = db.Orders.FirstOrDefault(o => o.Id == dto.OrderId);
    if (thisOrder == null)
    {
        return Results.NotFound();
    }
    thisOrder.PaymentTypeId = dto.PaymentTypeId;
    thisOrder.Tip = dto.Tip;
    thisOrder.Status = false;
    thisOrder.DateClosed = DateTime.Now;
    db.SaveChanges();
    return Results.Ok();
});

// Get Order Types
app.MapGet("/ordertypes", (Hip_hopDbContext db) =>
{
    return db.OrderTypes.ToList();
});

// Get Payment Types
app.MapGet("/paymenttypes", (Hip_hopDbContext db) =>
{
    return db.PaymentTypes.ToList();
});

// Search bar
app.MapGet("/orders/search", (Hip_hopDbContext db, string value) =>
{
    var searchResults = db.Orders
                .Where(o =>
                o.Name.ToLower().Contains(value.ToLower()) ||
                o.Phone.ToLower().Contains(value.ToLower()) ||
                o.Email.ToLower().Contains(value.ToLower()) ||
                o.OrderType.Type.ToLower().Contains(value.ToLower())
                )
                .ToList();

    return searchResults.Any() ? Results.Ok(searchResults) : Results.NotFound();
});

app.Run();



﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace hip_hop.Migrations
{
    [DbContext(typeof(Hip_hopDbContext))]
    [Migration("20240406190332_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("hip_hop.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("OrderItem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ItemPrice = 18.99m,
                            OrderItem = "Sausage Pepperoni Pizza"
                        },
                        new
                        {
                            Id = 2,
                            ItemPrice = 12.50m,
                            OrderItem = "Spicy Wings"
                        },
                        new
                        {
                            Id = 3,
                            ItemPrice = 15.99m,
                            OrderItem = "Hawaiian Pizza"
                        },
                        new
                        {
                            Id = 4,
                            ItemPrice = 21.99m,
                            OrderItem = "The Big Greek Pizza"
                        },
                        new
                        {
                            Id = 5,
                            ItemPrice = 14.99m,
                            OrderItem = "Honey Glazed Wings"
                        },
                        new
                        {
                            Id = 6,
                            ItemPrice = 9.99m,
                            OrderItem = "Cheesy Breadsticks"
                        },
                        new
                        {
                            Id = 7,
                            ItemPrice = 18.99m,
                            OrderItem = "So Much Cheese Pizza"
                        },
                        new
                        {
                            Id = 8,
                            ItemPrice = 24.99m,
                            OrderItem = "The Buffalo Wing Platter"
                        },
                        new
                        {
                            Id = 9,
                            ItemPrice = 22.99m,
                            OrderItem = "The Spicy Meaty Pizza"
                        });
                });

            modelBuilder.Entity("hip_hop.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("numeric");

                    b.Property<int>("OrderTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Tip")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderTypeId");

                    b.HasIndex("PaymentTypeId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "JohnGray@aol.com",
                            Name = "John Gray",
                            OrderTotal = 0m,
                            OrderTypeId = 2,
                            Phone = "789-456-3456",
                            Status = true,
                            Tip = 0m
                        },
                        new
                        {
                            Id = 2,
                            DateClosed = new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Jenna@yahoo.com",
                            Name = "Jenna Blue",
                            OrderTotal = 0m,
                            OrderTypeId = 1,
                            PaymentTypeId = 1,
                            Phone = "383-308-1093",
                            Status = false,
                            Tip = 7.50m
                        },
                        new
                        {
                            Id = 3,
                            Email = "SRed@hotmail.com",
                            Name = "Sarah Red",
                            OrderTotal = 0m,
                            OrderTypeId = 1,
                            Phone = "883-488-2239",
                            Status = true,
                            Tip = 0m
                        },
                        new
                        {
                            Id = 4,
                            DateClosed = new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Peach@peach.com",
                            Name = "Charles Peach",
                            OrderTotal = 0m,
                            OrderTypeId = 1,
                            PaymentTypeId = 2,
                            Phone = "112-456-3947",
                            Status = false,
                            Tip = 10.00m
                        });
                });

            modelBuilder.Entity("hip_hop.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("hip_hop.Models.OrderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Walk-In"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Call-In"
                        });
                });

            modelBuilder.Entity("hip_hop.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Cash"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Card"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Check"
                        });
                });

            modelBuilder.Entity("hip_hop.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Connect@JoeyEbach.com",
                            Name = "Joey Ebach"
                        });
                });

            modelBuilder.Entity("hip_hop.Models.Order", b =>
                {
                    b.HasOne("hip_hop.Models.OrderType", "OrderType")
                        .WithMany()
                        .HasForeignKey("OrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hip_hop.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId");

                    b.Navigation("OrderType");

                    b.Navigation("PaymentType");
                });

            modelBuilder.Entity("hip_hop.Models.OrderItem", b =>
                {
                    b.HasOne("hip_hop.Models.Item", "Item")
                        .WithMany("Order")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hip_hop.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("hip_hop.Models.Item", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("hip_hop.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}

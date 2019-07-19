﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SevensPizzaEntity;

namespace SevensPizzaEntity.Migrations
{
    [DbContext(typeof(SevensDBContext))]
    [Migration("20190718183841_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SevensPizzaEntity.CreditCard", b =>
                {
                    b.Property<int>("CardID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardName")
                        .IsRequired();

                    b.Property<int>("CecCode");

                    b.Property<int>("CustID");

                    b.Property<int>("DOE");

                    b.HasKey("CardID");

                    b.HasIndex("CustID");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("SevensPizzaEntity.Customer", b =>
                {
                    b.Property<int>("CustID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("CustID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SevensPizzaEntity.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustID");

                    b.Property<bool>("Delivery");

                    b.Property<string>("DeliveryAddress");

                    b.Property<DateTime>("OrderTime");

                    b.Property<string>("PaymentType")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<int>("TotalPizza");

                    b.HasKey("OrderID");

                    b.HasIndex("CustID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SevensPizzaEntity.Pizza", b =>
                {
                    b.Property<int>("PizzaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CheeseAmount")
                        .IsRequired();

                    b.Property<string>("CrustStyle")
                        .IsRequired();

                    b.Property<int>("OrderID");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Sauce")
                        .IsRequired();

                    b.Property<string>("SauceAmount")
                        .IsRequired();

                    b.Property<string>("Size")
                        .IsRequired();

                    b.HasKey("PizzaID");

                    b.HasIndex("OrderID");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("SevensPizzaEntity.Topping", b =>
                {
                    b.Property<int>("ToppingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("ToppingType");

                    b.HasKey("ToppingID");

                    b.ToTable("Toppings");
                });

            modelBuilder.Entity("SevensPizzaEntity.CreditCard", b =>
                {
                    b.HasOne("SevensPizzaEntity.Customer", "Cust")
                        .WithMany("CreditCards")
                        .HasForeignKey("CustID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SevensPizzaEntity.Order", b =>
                {
                    b.HasOne("SevensPizzaEntity.Customer", "Cust")
                        .WithMany("Orders")
                        .HasForeignKey("CustID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SevensPizzaEntity.Pizza", b =>
                {
                    b.HasOne("SevensPizzaEntity.Order", "order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

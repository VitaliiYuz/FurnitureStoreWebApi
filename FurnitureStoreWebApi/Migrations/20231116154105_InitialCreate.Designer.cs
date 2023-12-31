﻿// <auto-generated />
using System;
using FurnitureStoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FurnitureStoreWebApi.Migrations
{
    [DbContext(typeof(FurnitureStoreContext))]
    [Migration("20231116154105_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("CategoryId")
                        .HasName("PK__categori__D54EE9B4293B0FD4");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("phone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_name");

                    b.HasKey("CustomerId");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("order_date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("status");

                    b.HasKey("OrderId")
                        .HasName("PK__orders__4659622908BAB26A");

                    b.HasIndex("CustomerId");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_item_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderItemId")
                        .HasName("PK__order_it__3764B6BCC327EB04");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("order_items", (string)null);
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int")
                        .HasColumnName("stock_quantity");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("supplier_id");

                    b.HasKey("ProductId")
                        .HasName("PK__products__47027DF5404B5662");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("supplier_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("contact_email");

                    b.Property<string>("ContactName")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("contact_name");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("contact_phone");

                    b.Property<string>("Sname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("sname");

                    b.HasKey("SupplierId")
                        .HasName("PK__supplier__6EE594E8C2B20567");

                    b.ToTable("suppliers", (string)null);
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Order", b =>
                {
                    b.HasOne("FurnitureStoreWebApi.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_orders_customerID");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.OrderItem", b =>
                {
                    b.HasOne("FurnitureStoreWebApi.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_order_items_order_id");

                    b.HasOne("FurnitureStoreWebApi.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_order_items_product_id");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Product", b =>
                {
                    b.HasOne("FurnitureStoreWebApi.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_products_category_id");

                    b.HasOne("FurnitureStoreWebApi.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .IsRequired()
                        .HasConstraintName("FK_products_supplier_id");

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Product", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("FurnitureStoreWebApi.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

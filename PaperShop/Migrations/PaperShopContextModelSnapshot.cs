﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaperShop.BackPaper.DataAccess;

#nullable disable

namespace PaperShop.Migrations
{
    [DbContext(typeof(PaperShopContext))]
    partial class PaperShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("customers_pkey");

                    b.HasIndex(new[] { "Email" }, "customers_email_key")
                        .IsUnique();

                    b.ToTable("customers");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<DateOnly?>("DeliveryDate")
                        .HasColumnType("date")
                        .HasColumnName("delivery_date");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("order_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("pending")
                        .HasColumnName("status");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("double precision")
                        .HasColumnName("total_amount");

                    b.HasKey("Id")
                        .HasName("orders_pkey");

                    b.HasIndex(new[] { "CustomerId" }, "IX_orders_customer_id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.OrderEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("order_entries_pkey");

                    b.HasIndex(new[] { "OrderId" }, "IX_order_entries_order_id");

                    b.HasIndex(new[] { "ProductId" }, "IX_order_entries_product_id");

                    b.ToTable("order_entries");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Paper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Discontinued")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("discontinued");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<int>("Stock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("stock");

                    b.HasKey("Id")
                        .HasName("paper_pkey");

                    b.HasIndex(new[] { "Name" }, "unique_product_name")
                        .IsUnique();

                    b.ToTable("paper");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.PaperProperty", b =>
                {
                    b.Property<int>("PaperId")
                        .HasColumnType("integer")
                        .HasColumnOrder(0);

                    b.Property<int>("PropertyId")
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    b.HasKey("PaperId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("paper_properties", (string)null);
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("property_name");

                    b.HasKey("Id")
                        .HasName("properties_pkey");

                    b.ToTable("properties");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Order", b =>
                {
                    b.HasOne("PaperShop.BackPaper.DataAccess.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("orders_customer_id_fkey");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.OrderEntry", b =>
                {
                    b.HasOne("PaperShop.BackPaper.DataAccess.Models.Order", "Order")
                        .WithMany("OrderEntries")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("order_entries_order_id_fkey");

                    b.HasOne("PaperShop.BackPaper.DataAccess.Models.Paper", "Product")
                        .WithMany("OrderEntries")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("order_entries_product_id_fkey");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.PaperProperty", b =>
                {
                    b.HasOne("PaperShop.BackPaper.DataAccess.Models.Paper", "Paper")
                        .WithMany("PaperProperties")
                        .HasForeignKey("PaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("paper_properties_paper_id_fkey");

                    b.HasOne("PaperShop.BackPaper.DataAccess.Models.Property", "Property")
                        .WithMany("PaperProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("paper_properties_property_id_fkey");

                    b.Navigation("Paper");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Order", b =>
                {
                    b.Navigation("OrderEntries");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Paper", b =>
                {
                    b.Navigation("OrderEntries");

                    b.Navigation("PaperProperties");
                });

            modelBuilder.Entity("PaperShop.BackPaper.DataAccess.Models.Property", b =>
                {
                    b.Navigation("PaperProperties");
                });
#pragma warning restore 612, 618
        }
    }
}

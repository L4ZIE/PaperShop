using Microsoft.EntityFrameworkCore;
using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.DataAccess
{
    public partial class PaperShopContext : DbContext
    {
        public PaperShopContext(DbContextOptions<PaperShopContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderEntry> OrderEntries { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperProperty> PaperProperties { get; set; }
        public virtual DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("customers_pkey");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orders_pkey");

                entity.Property(e => e.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Status).HasDefaultValue("pending");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("orders_customer_id_fkey");
            });

            modelBuilder.Entity<OrderEntry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("order_entries_pkey");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderEntries)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("order_entries_order_id_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderEntries)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("order_entries_product_id_fkey");
            });

            modelBuilder.Entity<Paper>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("paper_pkey");

                entity.Property(e => e.Discontinued).HasDefaultValue(false);
                entity.Property(e => e.Stock).HasDefaultValue(0);

                entity.HasMany(p => p.PaperProperties)
                    .WithOne(pp => pp.Paper)
                    .HasForeignKey(pp => pp.PaperId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("paper_properties_paper_id_fkey");

                entity.HasMany(p => p.OrderEntries)
                    .WithOne(oe => oe.Product)
                    .HasForeignKey(oe => oe.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("properties_pkey");

                entity.HasMany(p => p.PaperProperties)
                    .WithOne(pp => pp.Property)
                    .HasForeignKey(pp => pp.PropertyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("paper_properties_property_id_fkey");
            });

            modelBuilder.Entity<PaperProperty>()
                .HasKey(pp => new { pp.PaperId, pp.PropertyId });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

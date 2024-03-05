
using ApiPetShop.Models;
using ApiPetShop.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApiPetShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Service_Bill> Service_Bills { get; set; }
        public DbSet<Product_Bill> product_Bills { get; set; }
        public DbSet<Product_Cart> product_Carts { get; set; }
        public DbSet<Service_Cart> service_Carts { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<ADV> ADVs { get; set; }
        public DbSet<Contact> contacts  { get; set; }
        public DbSet<Service_Detail> Service_Details { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // quan he nhiu nhiu bill va product bill
            builder.Entity<Bill>(b =>
            {
                b.ToTable("Bill");
                b.HasKey(dh => dh.Id);
                b.Property(dh => dh.dateTime).HasDefaultValueSql("getutcdate()");
            });
            builder.Entity<Product_Bill>(pb =>
            {
                pb.ToTable("Product_Bill");
                pb.HasKey(e => new { e.IdProduct, e.IdBill });
                pb.HasOne(e => e.bill)
                    .WithMany(e => e.product_Bills)
                    .HasForeignKey(e => e.IdBill)
                    .HasConstraintName("FK_ProductBill_Bill");
                pb.HasOne(e => e.product)
                    .WithMany(e => e.product_Bills)
                    .HasForeignKey(e => e.IdProduct)
                    .HasConstraintName("FK_ProductBill_Product");
            });
            builder.Entity<Service_Bill>(pb =>
            {
                pb.ToTable("Service_Bill");
                pb.HasKey(e => new { e.IdService, e.IdBill });
                pb.HasOne(e => e.bill)
                    .WithMany(e => e.Service_Bills)
                    .HasForeignKey(e => e.IdBill)
                    .HasConstraintName("FK_ServiceBill_Bill");
                pb.HasOne(e => e.service)
                    .WithMany(e => e.service_Bills)
                    .HasForeignKey(e => e.IdService)
                    .HasConstraintName("FK_ServiceBill_Product");
            });

            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("ApplicationUser");
                b.HasKey(dh => dh.Id);
               
            });
            builder.Entity<Product_Cart>(pb =>
            {
                pb.ToTable("Product_Cart");
                pb.HasKey(e => new { e.IdUser, e.IdProduct });
                pb.HasOne(e => e.applicationUser)
                    .WithMany(e => e.Product_Carts)
                    .HasForeignKey(e => e.IdUser)
                    .HasConstraintName("FK_ProductCart_Cart");
                pb.HasOne(e => e.product)
                    .WithMany(e => e.product_Carts)
                    .HasForeignKey(e => e.IdProduct)
                    .HasConstraintName("FK_ProductCart_Product");
            });
            builder.Entity<Service_Cart>(pb =>
            {
                pb.ToTable("Service_Cart");
                pb.HasKey(e => new { e.IdUser, e.IdServie });
                pb.HasOne(e => e.applicationUser)
                    .WithMany(e => e.Service_Carts)
                    .HasForeignKey(e => e.IdUser)
                    .HasConstraintName("FK_ServiceCart_Cart");
                pb.HasOne(e => e.service)
                    .WithMany(e => e.service_Carts)
                    .HasForeignKey(e => e.IdServie)
                    .HasConstraintName("FK_ServiceCart_Product");
            });
            builder.Entity<Service_Detail>(pb =>
            {
                pb.ToTable("Service_Detail");
                pb.HasKey(e => new { e.IdService, e.IdTime });
                pb.HasOne(e => e.service)
                    .WithMany(e => e.service_Details)
                    .HasForeignKey(e => e.IdService)
                    .HasConstraintName("FK_ServiceDetail_Service");
                pb.HasOne(e => e.time)
                    .WithMany(e => e.service_Details)
                    .HasForeignKey(e => e.IdTime)
                    .HasConstraintName("FK_ServiceDetail_Time");
            });
            base.OnModelCreating(builder);
        }

    }
}

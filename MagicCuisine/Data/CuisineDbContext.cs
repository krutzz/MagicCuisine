using Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Data
{
    public class CuisineDbContext : IdentityDbContext<User>
    {
        public CuisineDbContext()
                : base("CuisineDBConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Address> Addresses { get; set; }

        public static CuisineDbContext Create()
        {
            return new CuisineDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.CreateAddresseModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void CreateAddresseModels(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .ToTable("Countries");

            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
               .HasMaxLength(150)
               .IsRequired()
               .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                    new IndexAttribute("IX_UniqueName") { IsUnique = true }));

            modelBuilder.Entity<Town>()
                .Property(t => t.Name)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.PostalCode)
                .HasMaxLength(10);

            modelBuilder.Entity<Address>()
                .Property(a => a.Building)
                .HasMaxLength(10);

            modelBuilder.Entity<Address>()
                .Property(a => a.Floor)
                .HasMaxLength(10);

            modelBuilder.Entity<Address>()
                .Property(a => a.Entrance)
                .HasMaxLength(10);

            modelBuilder.Entity<Address>()
                .Property(a => a.Flat)
                .HasMaxLength(10);
        }
    }
}
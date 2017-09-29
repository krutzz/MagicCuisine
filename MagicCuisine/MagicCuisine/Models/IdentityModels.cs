using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Data.Models;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicCuisine.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public virtual Address Address { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
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
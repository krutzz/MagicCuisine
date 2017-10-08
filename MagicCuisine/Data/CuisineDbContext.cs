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

        public IDbSet<Recipe> Recipes { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static CuisineDbContext Create()
        {
            return new CuisineDbContext();
        }

    }
}
using Data.Models;
using System.Data.Entity;

namespace Data
{
    public class CuisineContext : DbContext
    {
        public CuisineContext()
                : base("CuisineDBConnection")
        {
        }

        public IDbSet<Test> Tests { get; set; }
    }
}

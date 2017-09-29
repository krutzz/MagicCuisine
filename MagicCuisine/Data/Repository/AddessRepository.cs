using Data.Models;
using Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AddessRepository : Repository<Address>, IAddessRepository
    {
        public AddessRepository(CuisineContext context)
            : base(context)
        {
        }
    }
}

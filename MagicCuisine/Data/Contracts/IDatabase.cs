using Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IDataBase
    {
        int Complete();

        ICountryRepository Countries { get; }

        ITownRepository Towns { get; }

        IAddessRepository Addesses { get; }
    }
}

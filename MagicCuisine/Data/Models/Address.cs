using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Address
    {
        public int ID { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Flat { get; set; }

        public string PostalCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual Town Town { get; set; }
    }
}

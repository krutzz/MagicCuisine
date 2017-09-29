using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Town
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }
    }
}

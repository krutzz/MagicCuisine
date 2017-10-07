using System;

namespace Data.Models
{
    public class Country
    {
        public Country()
        {
            this.ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }

        public string Name { get; set; }
    }

}

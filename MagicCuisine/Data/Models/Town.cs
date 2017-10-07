using System;

namespace Data.Models
{
    public class Town
    {
        public Town()
        {
            this.ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }
    }
}

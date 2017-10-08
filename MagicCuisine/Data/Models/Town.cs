using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Town
    {
        public Town()
        {
            this.ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }
    }
}

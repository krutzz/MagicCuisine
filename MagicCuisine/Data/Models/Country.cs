using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Country
    {
        public Country()
        {
            this.ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }

        [Required]
        [MaxLength(150)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}

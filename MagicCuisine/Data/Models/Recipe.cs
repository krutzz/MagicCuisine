﻿using Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Recipe : IDeletable
    {
        public Recipe()
        {
            this.ID = Guid.NewGuid();
            this.IsDeleted = false;
        }

        public Recipe(string avatar, string title, string description)
            :this()
        {
            this.Avatar = avatar;
            this.Title = title;
            this.Description = description;
        }

        public Guid ID { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Avatar { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

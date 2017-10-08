using Data.Models.Contracts;
using System;

namespace Data.Models
{
    public class Comment: IDeletable
    {
        public Comment()
        {
            this.ID = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.IsDeleted = false;
        }

        public Comment(User user, Recipe recipe, string Description)
            :this()
        {
            this.User = user;
            this.Recipe = recipe;
            this.Description = Description;
        }

        public Guid ID { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}

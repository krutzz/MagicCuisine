using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class CommentServiceModel
    {
        public CommentServiceModel(string description, bool isDeleted)
        {
            this.Description = description;
            this.IsDeleted = isDeleted;
        }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}

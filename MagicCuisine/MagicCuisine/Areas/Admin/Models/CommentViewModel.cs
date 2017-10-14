using MagicCuisine.Infrastructure;
using System;
using AutoMapper;
using Data.Models;

namespace MagicCuisine.Areas.Admin.Models
{
    public class CommentViewModel : IMapFrom<Comment>//, IHaveCustomMappings
    {
        public Guid ID { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string UserEmail { get; set; }

        public string RecipeTitle { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                 .ForMember(c => c.UserEmail, cfg => cfg.MapFrom(x => x.User.Email));

            configuration.CreateMap<Comment, CommentViewModel>()
                  .ForMember(c => c.RecipeTitle, cfg => cfg.MapFrom(x => x.Recipe.Title));
        }

    }
}
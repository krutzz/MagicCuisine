using System;
using AutoMapper;
using Data.Models;
using MagicCuisine.Infrastructure;

namespace MagicCuisine.Models
{
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public Guid ID { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string UserEmail { get; set; }

        public string UserAvatar { get; set; }

        public Guid RecipeID { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                    .ForMember(c => c.UserAvatar, cfg => cfg.MapFrom(x => x.User.Avatar));

            configuration.CreateMap<Comment, CommentViewModel>()
                    .ForMember(c => c.UserEmail, cfg => cfg.MapFrom(x => x.User.Email));

            configuration.CreateMap<Comment, CommentViewModel>()
                  .ForMember(c => c.RecipeID, cfg => cfg.MapFrom(x => x.Recipe.ID));
        }
    }
}
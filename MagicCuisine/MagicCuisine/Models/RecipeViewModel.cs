using Data.Models;
using MagicCuisine.Infrastructure;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

namespace MagicCuisine.Models
{
    public class RecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public Guid ID { get; set; }

        public bool IsDeleted { get; set; }

        public string Avatar { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeViewModel>()
                .ForMember(r => r.Comments, cfg => cfg.MapFrom(c => Mapper.Map<ICollection<CommentViewModel>>(c.Comments.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Date))));
        }
    }
}
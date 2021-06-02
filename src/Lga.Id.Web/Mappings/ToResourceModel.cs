using AutoMapper;
using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Web.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.Mappings
{    
    public class ToResourceModel : Profile
    {
        public ToResourceModel()
        {
            CreateMap<ScoreResourceModel, Score>();
            CreateMap<SaveScoreResourceModel, Score>();
            CreateMap<SaveScoreDetailResourceModel, ScoreDetail>();

            //Just for code review to show how we map these fields.
            CreateMap<ScoreDetailResourceModel, ScoreDetail>()
                .ForMember(src => src.UsualResedantPopulation, opt => opt.MapFrom(src => src.UsualResedantPopulation)); 
          

        }
    }
}

using AutoMapper;
using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Web.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.Mappings
{
    public class FromResourceModel : Profile
    {
        public FromResourceModel()
        {

            CreateMap<Score, ScoreResourceModel>();
            CreateMap<ScoreDetail, ScoreDetailResourceModel>(); 

            CreateMap<Score, SaveScoreResourceModel>();
            CreateMap<ScoreDetail, SaveScoreDetailResourceModel>();      

            CreateMap<IEnumerable<Score>, IEnumerable<ScoreResourceModel>>();          

            
        }
    }
}

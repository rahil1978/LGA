using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Web.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.Mappings
{
    public static class CustomMappingHelper
    {
        internal static void MapScores(IEnumerable<Score> scores, List<ScoreResourceModel> resources)
        {
            foreach (var score in scores)
            {
                resources.Add(new ScoreResourceModel
                { 
                    Id = score.Id,
                    AdvantageDisadvantageScore = score.AdvantageDisadvantageScore,
                    DisadvantageScore = score.DisadvantageScore,
                    Year = score.Year
                   
                });
            }
        }

        internal static void MapScoreDetails(IEnumerable<ScoreDetail> scoreDetails, List<ScoreDetailResourceModel> resources)
        {
            foreach (var scoreDetail in scoreDetails)
            {
                resources.Add(new ScoreDetailResourceModel
                { 
                    Id = scoreDetail.Id, 
                     AdvantageDisadvantageDecile = scoreDetail.AdvantageDisadvantageDecile, 
                      DisadvantageDecile = scoreDetail.DisadvantageDecile, 
                       IndexOfEconomicResourcesDecile = scoreDetail.IndexOfEconomicResourcesDecile,
                        IndexOfEconomicResourcesScore = scoreDetail.IndexOfEconomicResourcesScore,
                        IndexOfEducationAndOccupationDecile = scoreDetail.IndexOfEducationAndOccupationDecile,
                         IndexOfEducationAndOccupationScore = scoreDetail.IndexOfEducationAndOccupationScore,
                         UsualResedantPopulation = scoreDetail.UsualResedantPopulation

                   
                });
            }
        }
    }
}



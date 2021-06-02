using Lga.Id.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lga.Id.Core.Entities.ScoreAggregate
{
    public class Score : BaseEntity, IAggregateRoot
    {        
        public int? DisadvantageScore { get; set; }
        public int? AdvantageDisadvantageScore { get; set; }
        public int Year { get; set; }
        public Location Location { get; set; }
        public List<ScoreDetail> ScoreDetails { get; set; }
       
        // public DateTime CreatedOn { get; set; }
        // public string CreatedBy { get; set; } 

        public Score()
        {
            //Only for EF
        }
        public Score(int? disadvantageScore, int? advantageDisadvantageScore, int year, Location location)
        {
            //Id = scoreId;
            DisadvantageScore = disadvantageScore;
            AdvantageDisadvantageScore = advantageDisadvantageScore;
            Year = year;
            Location = location;
            ScoreDetails = new List<ScoreDetail>();

        }      
        public void AddScoreDetail(Score score, int? advantageDisadvantageDecile, int? disadvantageDecile, int? indexOfEconomicResourcesScore,
            int? indexOfEconomicResourcesDecile, int? indexOfEducationAndOccupationScore, int? indexOfEducationAndOccupationDecile,
            decimal usualResedantPopulation)
        {
            if (ScoreDetails == null)
            {
                ScoreDetails = new List<ScoreDetail>();
            }

            if (ScoreDetails != null && !ScoreDetails.Any() /*&& !ScoreDetails.Any(i => score.Location == score.Location)*/)
            {
                ScoreDetails.Add(new ScoreDetail(score, advantageDisadvantageDecile, disadvantageDecile, indexOfEconomicResourcesScore,
                 indexOfEconomicResourcesDecile, indexOfEducationAndOccupationScore,indexOfEducationAndOccupationDecile, usualResedantPopulation));
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Entities.ScoreAggregate
{
    public class ScoreDetail : BaseEntity
    {
		public Score Score { get; set; }
		public int? AdvantageDisadvantageDecile { get; set; }
		public int? DisadvantageDecile { get; set; }
		public int? IndexOfEconomicResourcesScore { get; set; }
		public int? IndexOfEconomicResourcesDecile { get; set; }
		public int? IndexOfEducationAndOccupationScore { get; set; }
		public int? IndexOfEducationAndOccupationDecile { get; set; }
		public decimal UsualResedantPopulation { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public ScoreDetail()
        {
            //For EF only
        }


        public ScoreDetail(Score score, int? advantageDisadvantageDecile, int? disadvantageDecile, int? indexOfEconomicResourcesScore,
           int? indexOfEconomicResourcesDecile, int? indexOfEducationAndOccupationScore, int? indexOfEducationAndOccupationDecile,
           decimal usualResedantPopulation)
        {
            Score = score;
            AdvantageDisadvantageDecile = advantageDisadvantageDecile;
            DisadvantageDecile = disadvantageDecile;
            IndexOfEconomicResourcesScore = indexOfEconomicResourcesScore;
            IndexOfEconomicResourcesDecile = indexOfEconomicResourcesDecile;
            IndexOfEducationAndOccupationScore = indexOfEducationAndOccupationScore;
            IndexOfEducationAndOccupationDecile = indexOfEducationAndOccupationDecile;
            UsualResedantPopulation = usualResedantPopulation;
        }
    }
}

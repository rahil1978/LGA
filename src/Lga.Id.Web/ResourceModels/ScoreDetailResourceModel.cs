using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lga.Id.Web.ResourceModels
{
    public class ScoreDetailResourceModel
    {
        public int Id { get; set; }

		//public Score Score { get; set; }
		public int? AdvantageDisadvantageDecile { get; set; }
		public int? DisadvantageDecile { get; set; }
		public int? IndexOfEconomicResourcesScore { get; set; }
		public int? IndexOfEconomicResourcesDecile { get; set; }
		public int? IndexOfEducationAndOccupationScore { get; set; }
		public int? IndexOfEducationAndOccupationDecile { get; set; }
		public decimal UsualResedantPopulation { get; set; }

	}
}

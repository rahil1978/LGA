using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ResourceModels
{
    public class SaveScoreDetailResourceModel
    {                
        public int? AdvantageDisadvantageDecile { get; set; }
        public int? DisadvantageDecile { get; set; }
        public int? IndexOfEconomicResourcesScore { get; set; }
        public int? IndexOfEconomicResourcesDecile { get; set; }
        public int? IndexOfEducationAndOccupationScore { get; set; }
        public int? IndexOfEducationAndOccupationDecile { get; set; }
        
        [Required]
        [MaxLength(50)]
        public decimal UsualResedantPopulation { get; set; }


    }
}

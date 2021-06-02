using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ResourceModels
{
    public class ScoreResourceModel
    {
        public int Id { get; set; }       
        public int? DisadvantageScore { get; set; }
        public int? AdvantageDisadvantageScore { get; set; }
        public int Year { get; set; }
        //public Location Location { get; set; }
        //public List<ScoreDetail> ScoreDetails { get; set; }

    }
}





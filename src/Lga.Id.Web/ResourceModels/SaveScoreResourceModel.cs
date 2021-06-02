using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ResourceModels
{
    public class SaveScoreResourceModel
    {
        //public int Id { get; set; }
        public int? DisadvantageScore { get; set; }
        public int? AdvantageDisadvantageScore { get; set; }
        [Required]
        public int Year { get; set; }
    }
}

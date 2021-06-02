using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Lga.Id.Core.Entities;

namespace Lga.Id.UnitTests.EntitiesTests
{
    public class AddScoreDetailsInProductTests
    {
        
        //for Score
        private readonly int _DisadvantageScore = 30;
        private readonly int _AdvantageDisadvantageScore = 40;
        private readonly int _Year = 1991;
        readonly Location _loc = new Location() { Id = 1, Code = 10, PlaceName = "XYZ", State = new State() { Id = 1, Median = 18.0m, StateName = "VIC" } };

        //for Score Detail
        private readonly Score _Score = new Score();
        private readonly int? AdvantageDisadvantageDecile = 10;
        private readonly int? DisadvantageDecile = 20;
        private readonly int? IndexOfEconomicResourcesScore = 30;
        private readonly int? IndexOfEconomicResourcesDecile = 6;
        private readonly int? IndexOfEducationAndOccupationScore = 50;
        private readonly int? IndexOfEducationAndOccupationDecile = 8;
        private readonly decimal UsualResedantPopulation = 0;         


        [Fact]
        public void AddsScoreDetailIfNotPresent()
        {

            //Arrange

            //Score + ScoreDetail            
            int disadvantageScore = 30;
            int advantageDisadvantageScore = 40;
            int scoreYear = 1991;
            var score = new Score(disadvantageScore, advantageDisadvantageScore, scoreYear, new Location() { Id = 1, Code = 10, PlaceName = "XYZ", State = new State() { Id = 1, Median = 18.0m, StateName = "VIC" } });
                
            int? AdvantageDisadvantageDecile = 10;
            int? DisadvantageDecile = 20;
            int? IndexOfEconomicResourcesScore = 30;
            int? IndexOfEconomicResourcesDecile = 40;
            int? IndexOfEducationAndOccupationScore = 50;
            int? IndexOfEducationAndOccupationDecile = 60;
            decimal UsualResedantPopulation = 70; 

            //Act    
            score.AddScoreDetail(score, AdvantageDisadvantageDecile, DisadvantageDecile, IndexOfEconomicResourcesScore, IndexOfEconomicResourcesDecile, IndexOfEducationAndOccupationScore, IndexOfEducationAndOccupationDecile, UsualResedantPopulation);

            //Assert
            var firstScoreDetail = score.ScoreDetails.First();
            Assert.Equal(AdvantageDisadvantageDecile, firstScoreDetail.AdvantageDisadvantageDecile);
            Assert.Equal(DisadvantageDecile, firstScoreDetail.DisadvantageDecile);
            Assert.Equal(IndexOfEconomicResourcesScore, firstScoreDetail.IndexOfEconomicResourcesScore);  
            Assert.Equal(IndexOfEconomicResourcesScore, firstScoreDetail.IndexOfEconomicResourcesScore);
            

        }

        [Fact]
        public void AddMoreProductOptions()
        {
            //arrange
            int disadvantageScore = 30;
            int advantageDisadvantageScore = 40;
            int scoreYear = 1991;
            var score = new Score(disadvantageScore, advantageDisadvantageScore, scoreYear, new Location() { Id = 1, Code = 10, PlaceName = "XYZ", State = new State() { Id = 1, Median = 18.0m, StateName = "VIC" } });

            //Act
            score.AddScoreDetail(score, AdvantageDisadvantageDecile, DisadvantageDecile, IndexOfEconomicResourcesScore, IndexOfEconomicResourcesDecile, IndexOfEducationAndOccupationScore, IndexOfEducationAndOccupationDecile, UsualResedantPopulation);
            score.AddScoreDetail(score, AdvantageDisadvantageDecile + 1, DisadvantageDecile + 1, IndexOfEconomicResourcesScore + 1, IndexOfEconomicResourcesDecile + 1, IndexOfEducationAndOccupationScore, IndexOfEducationAndOccupationDecile, UsualResedantPopulation);

            //Assert
            var firstScoreDetail = score.ScoreDetails.First();
            Assert.Equal(AdvantageDisadvantageDecile, firstScoreDetail.AdvantageDisadvantageDecile);
            Assert.Equal(DisadvantageDecile, firstScoreDetail.DisadvantageDecile);
            Assert.Equal(IndexOfEconomicResourcesScore, firstScoreDetail.IndexOfEconomicResourcesScore);
            

            var secondScoreDetail = score.ScoreDetails.First();
            Assert.Equal(AdvantageDisadvantageDecile + 1, secondScoreDetail.AdvantageDisadvantageDecile);
            Assert.Equal(DisadvantageDecile + 1, secondScoreDetail.DisadvantageDecile);
            Assert.Equal(IndexOfEconomicResourcesScore + 1, secondScoreDetail.IndexOfEconomicResourcesScore);
            
        }
    }


}

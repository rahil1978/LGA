using Lga.Id.Core.Entities;
using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Core.Interfaces.Services;
using Lga.Id.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Lga.Id.UnitTests.EntitiesTests
{

    //FOR CODE REVIEW THIS IS JUST TO SHOW CASE AND DUE TO LACK OF TIME A SAMPLE TEST IS WRITTEN
    public class ScoreCreationTests
    {
        //for Score
        private readonly int _DisadvantageScore = 30;
        private readonly int _AdvantageDisadvantageScore = 40;
        private readonly int _Year = 1991;
        readonly Location _loc = new Location() { Id = 1, Code = 10, PlaceName = "XYZ", State = new State() { Id = 1, Median = 18.0m, StateName = "VIC" } };
                      

        [Fact]
        public void AddsNewProduct()
        {
            //Arrange
            int disadvantageScore = 30;
            int advantageDisadvantageScore = 40;
            int scoreYear = 1991;

            //Act ~Test constructor
            var score = new Score(disadvantageScore, advantageDisadvantageScore, scoreYear, new Location() { Id = 1, Code = 10, PlaceName = "XYZ", State = new State() { Id = 1, Median = 18.0m, StateName = "VIC" } });

            //Assert            
            Assert.Equal(_DisadvantageScore, score.DisadvantageScore);
            Assert.Equal(_AdvantageDisadvantageScore, score.DisadvantageScore);

        }

       
    }
}

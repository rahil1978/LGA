using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Specifications
{
    public class ScoreWithScoreDetailSpecification : BaseSpecification<Score>
    {
        /*
         There should be these endpoints:         
         7. `GET /score/{id}/detail` - finds all details for a specified score.
         8. `GET /score/{id}/detail/{optionId}` - finds the specified score detail for the specified product.         
        */

        public ScoreWithScoreDetailSpecification(int scoreId)
        : base(i => (i.Id == scoreId))
        {
            AddInclude(b => b.ScoreDetails);
        }


        public ScoreWithScoreDetailSpecification(int scoreId, int scoreDetailId)
       : base(i => (i.Id == scoreId))
        {
            AddInclude(b => b.ScoreDetails);
        }

    }
}

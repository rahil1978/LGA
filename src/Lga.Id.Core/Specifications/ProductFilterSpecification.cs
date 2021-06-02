using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Specifications
{ 
    public sealed class ScoreFilterSpecification : BaseSpecification<Score>
    {
        /*        
        2. `GET /score/{id}` - gets the project that matches the specified ID - ID is a GUID.
       */

       
        public ScoreFilterSpecification(int? scoreId)
            : base(i => (!scoreId.HasValue || i.Id == scoreId))
        {
        }    

    }
}

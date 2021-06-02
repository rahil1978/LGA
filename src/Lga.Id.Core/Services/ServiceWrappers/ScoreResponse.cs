using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Services.ServiceWrappers
{ 
    public class ScoreResponse : BaseResponse<Score>
    {
        public ScoreResponse(Score score) : base(score) { }
        public ScoreResponse(string message) : base(message) { }
    }
}

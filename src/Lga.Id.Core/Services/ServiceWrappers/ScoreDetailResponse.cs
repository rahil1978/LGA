using Lga.Id.Core.Entities.ScoreAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Services.ServiceWrappers
{  
    public class ScoreDetailResponse : BaseResponse<ScoreDetail>
    {

        public ScoreDetailResponse(ScoreDetail scoreDetail) : base(scoreDetail)
        { }

        public ScoreDetailResponse(string message) : base(message)
        { }
    }
}

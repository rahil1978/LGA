using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Exceptions
{
    public class ScoreDetailException : Exception
    {     
        protected ScoreDetailException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public ScoreDetailException(string message) : base(message)
        {
        }

        public ScoreDetailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ScoreDetailException(int scoreId) : base($"Score Details Not found for Id: {scoreId}")
        {
        }
    }
}

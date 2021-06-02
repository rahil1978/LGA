using System;
using System.Collections.Generic;
using System.Text;


namespace Lga.Id.Core.Exceptions
{
    public class ScoreException : Exception
    {
        public ScoreException()
        { }

        public ScoreException(string message)
            : base(message)
        { }

        public ScoreException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

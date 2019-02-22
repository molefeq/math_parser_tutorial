using System;
using System.Runtime.Serialization;

namespace MathParserTutorial
{
    public class MathParserFormulaException : Exception
    {
        private string formular;

        public MathParserFormulaException() : base()
        {
        }

        public MathParserFormulaException(string message) : base(message)
        {
        }

        public MathParserFormulaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MathParserFormulaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

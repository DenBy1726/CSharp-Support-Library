using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AI.Util.Exceptions
{
    public class TooLongException : Exception
    {
        public TooLongException()
        {
        }

        public TooLongException(string message) : base(message)
        {
        }

        public TooLongException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

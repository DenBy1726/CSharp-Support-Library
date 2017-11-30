using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AI.Util.Exceptions
{
    public class LogicalException : Exception
    {
        public LogicalException()
        {
        }

        public LogicalException(string message) : base(message)
        {
        }

        public LogicalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LogicalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CarRental.API.Exceptions
{
    public class IncorrectValueException : Exception, ISerializable
    {
        public IncorrectValueException()
            : base()
        {

        }
        public IncorrectValueException(string message)
            : base(message)
        {

        }
        public IncorrectValueException(string message, Exception inner)
            : base(message, inner)
        {

        }
        public IncorrectValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}

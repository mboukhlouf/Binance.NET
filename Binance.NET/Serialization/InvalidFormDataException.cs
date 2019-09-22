using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Serialization
{
    class InvalidFormDataException : Exception
    {
        public InvalidFormDataException() : base()
        {
        }

        public InvalidFormDataException(String message) : base(message)
        {
        }

        public InvalidFormDataException(String message, Exception inner) : base(message, inner)
        {
        }
    }
}

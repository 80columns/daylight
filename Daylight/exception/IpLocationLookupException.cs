using System;
using System.Runtime.Serialization;

namespace Daylight.exception {
    [Serializable]
    internal class IpLocationLookupException : Exception {
        public IpLocationLookupException() { }

        public IpLocationLookupException(string message) : base(message) { }

        public IpLocationLookupException(string message, Exception innerException) : base(message, innerException) { }

        protected IpLocationLookupException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
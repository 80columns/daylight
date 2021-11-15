using System;
using System.Runtime.Serialization;

namespace Daylight.exception {
    [Serializable]
    internal class SunriseSunsetLookupException : Exception {
        public SunriseSunsetLookupException() { }

        public SunriseSunsetLookupException(string message) : base(message) { }

        public SunriseSunsetLookupException(string message, Exception innerException) : base(message, innerException) { }

        protected SunriseSunsetLookupException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
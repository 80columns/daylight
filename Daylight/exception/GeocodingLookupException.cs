using System;
using System.Runtime.Serialization;

namespace Daylight.exception {
    [Serializable]
    internal class GeocodingLookupException : Exception {
        public GeocodingLookupException() { }

        public GeocodingLookupException(string message) : base(message) { }

        public GeocodingLookupException(string message, Exception innerException) : base(message, innerException) { }

        protected GeocodingLookupException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class RecommendedTracksNullException : HttpRequestException
    {
        public RecommendedTracksNullException()
        {
        }

        public RecommendedTracksNullException(string? message) : base(message)
        {
        }

        public RecommendedTracksNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

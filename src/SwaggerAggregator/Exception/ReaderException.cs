using System;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator
{
    public class ReaderException : Exception
    {
        public ReaderException(string message) : base(message) { }

        public ReaderException(string message, string endpoint, IList<OpenApiError> errors) : base(message) 
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public string Endpoint { get; set; }

        public IList<OpenApiError> Errors { get; set; }
    }
}

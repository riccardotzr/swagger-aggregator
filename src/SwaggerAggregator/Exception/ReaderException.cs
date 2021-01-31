using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public class ReaderException : Exception
    {
        public ReaderException(string message, IList<OpenApiError> errors) : base(message) 
        {
            Errors = errors;
        }

        public IList<OpenApiError> Errors { get; private set; }
    }
}

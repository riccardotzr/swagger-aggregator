using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public class ValidationError
    {
        public string Message { get; set; }

        public string Pointer { get; set; }
    }

    public class BadRequestResponse
    {
        public string Message { get; set; }

        public string Endpoint { get; set; }

       public List<ValidationError> Errors { get; set; }
    }
}

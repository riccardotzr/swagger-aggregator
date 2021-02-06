using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public class InternalServerErrorResponse
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}

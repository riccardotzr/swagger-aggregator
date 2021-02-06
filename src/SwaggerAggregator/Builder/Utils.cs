using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public static class Utils
    {
        public static ParameterLocation ToParameterLocation(this string location)
        {
            switch (location)
            {
                case "Query":
                    return ParameterLocation.Query;
                case "Header":
                    return ParameterLocation.Header;
                case "Path":
                    return ParameterLocation.Path;
                case "Cookie":
                    return ParameterLocation.Cookie;
                default:
                    return ParameterLocation.Header;
            }
        }
    }
}

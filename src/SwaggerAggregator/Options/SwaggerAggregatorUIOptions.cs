using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public static class FileExtension
    {
        public const string Json = "json";
        public const string Yaml = "yaml";
    }

    public class SwaggerAggregatorUIOptions
    {
        public string RoutePrefix { get; set; } = "documentation";

        public string FileName { get; set; } = "swagger";

        public string FileExtension { get; set; } = "yaml";

        public List<string> Versions { get; set; } = new List<string> { "v1" };
    }
}

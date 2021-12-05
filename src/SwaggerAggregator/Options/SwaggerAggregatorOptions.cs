using System;

namespace SwaggerAggregator
{
    public class SwaggerAggregatorOptions
    {
        public string Title { get; set; } = "Swagger Aggregator";

        public string Description { get; set; } = "";

        public string Version { get; set; } = "1.0.0";

        public string Route { get; set; } = "documentation/swagger.{json|yaml}";

        public List<Service> Services { get; set; } = new List<Service>();
    }

    public class Service
    {
        public string Type { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        public string Prefix { get; set; }

        public List<ApiPath> IncludePaths { get; set; }

        public List<ApiPath> EscludePaths { get; set; }

    }

    public class ApiPath
    {
        public string Path { get; set; }
    }

}


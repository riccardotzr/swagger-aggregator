using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerAggregatorOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Info Info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Server> Servers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Service> Services { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Info
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Server
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Service
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool RemoveApiPrefix { get; set; }
    }
}

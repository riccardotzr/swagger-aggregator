using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAggregator.Test
{
    internal static class Resources
    {
        private static string GetPath(string fileName)
        {
            var pathSeparator = ".";
            //var result = $"{typeof(Resources).Namespace}{pathSeparator}{fileName.Replace('/', '.')}";
            //return result;
    
            //var path = Path.Combine(typeof(Resources).Namespace, fileName);
            return typeof(Resources).Namespace + pathSeparator + fileName.Replace('/', '.');
            //return path;
        }

        public static string GetFileContent(string fileName)
        {
            var path = GetPath(fileName);
            var stream = typeof(Resources).Assembly.GetManifestResourceStream(path);

            if (stream == null)
            {
                var message = $"The embedded resource {fileName} was not found in {path}";
                throw new FileNotFoundException(message);
            }

            using var reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            return result;
        }
    }
}

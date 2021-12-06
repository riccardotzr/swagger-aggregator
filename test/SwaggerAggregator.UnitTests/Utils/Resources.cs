using System;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace SwaggerAggregator.UnitTests
{
    internal static class Resources
    {
        private static string GetPath(string fileName)
        {
            var pathSeparator = ".";
            return typeof(Resources).Namespace + pathSeparator + fileName.Replace('/', '.');
        }

        public static Stream GetStreamContent(string fileName)
        {
            var path = GetPath(fileName);
            var stream = typeof(Resources).Assembly.GetManifestResourceStream(path);

            if (stream == null)
            {
                var message = $"The embedded resource {fileName} was not found in {path}";
                throw new FileNotFoundException(message);
            }

            return stream;
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

        public static OpenApiDocument GetYamlFile(string fileName)
        {
            var path = GetPath(fileName);
            var stream = typeof(Resources).Assembly.GetManifestResourceStream(path);

            if (stream == null)
            {
                var message = $"The embedded resource {fileName} was not found in {path}";
                throw new FileNotFoundException(message);
            }

            var result = new OpenApiStreamReader().Read(stream, out var diagnostic);

            return result;
        }
    }
}


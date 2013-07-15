using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace HandlebarsDotNet
{
    public class HandlebarsBundleTransform : IBundleTransform
    {
        public HandlebarsBundleTransform(string jsDictionaryName = "templates", bool minifyTemplates = true, string rootDirectory = "/")
        {
            JsDictionaryName = jsDictionaryName;
            MinifyTemplates = minifyTemplates;
            RootDirectory = rootDirectory;

            if (!RootDirectory.StartsWith("/"))
                RootDirectory = "/" + RootDirectory;
        }

        public string JsDictionaryName { get; set; }
        public bool MinifyTemplates { get; set; }
        public string RootDirectory { get; set; }

        public void Process(BundleContext context, BundleResponse response)
        {
            if (!response.Files.Any())
                return;

            var compiler = new HandlebarsCompiler();

            var builder = new StringBuilder();
            builder.AppendFormat("var {0} = ", JsDictionaryName);
            builder.Append("{");

            var i = 0;
            foreach (var file in response.Files)
            {
                var virtualFilePath = String.Format("~{0}", file.VirtualFile.VirtualPath);
                var fileExtension = virtualFilePath.Substring(virtualFilePath.LastIndexOf(".", StringComparison.OrdinalIgnoreCase));

                var templateKey = virtualFilePath
                    .Replace("~", String.Empty)
                    .Replace(fileExtension, String.Empty)
                    .Replace(RootDirectory, String.Empty);

                var path = context.HttpContext.Server.MapPath(virtualFilePath);
                var template = compiler.Precompile(File.ReadAllText(path));
                
                builder.AppendFormat("'{0}' : Handlebars.template({1})", templateKey, template);

                i++;

                if (i < response.Files.Count())
                    builder.Append(",");

                builder.Append(Environment.NewLine);
            }

            builder.Append("}");

            var content = builder.ToString();
            if (MinifyTemplates)
            {
                var minifier = new Minifier();
                var c = minifier.MinifyJavaScript(content);
                if (minifier.ErrorList.Count <= 0)
                    content = c;
            }

            response.ContentType = "text/javascript";
            response.Cacheability = HttpCacheability.Public;
            response.Content = content;
        }
    }
}

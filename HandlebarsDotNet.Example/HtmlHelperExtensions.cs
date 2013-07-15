using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace HandlebarsDotNet.Example
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RenderHandlebarsTemplates(this HtmlHelper helper, string path = "", bool noTemplateName = false)
        {
            //TODO: Implement caching.
            return Scripts.Render(path);
        }
    }
}
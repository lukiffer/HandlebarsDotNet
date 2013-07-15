using System.Reflection;
using System.IO;

namespace HandlebarsDotNet
{
    public class HandlebarsCompiler
    {
        private readonly ScriptEngine _scriptEngine;
        private readonly ParsedScript _parsedScript;

        public HandlebarsCompiler()
        {
            _scriptEngine = new ScriptEngine("jscript");
            _parsedScript = _scriptEngine.Parse(LoadResource());
        }
        
        private static string LoadResource()
        {
            var asm = Assembly.GetCallingAssembly();
            var stream = asm.GetManifestResourceStream("HandlebarsDotNet.compiler.js");
            return stream != null 
                ? new StreamReader(stream).ReadToEnd() 
                : null;
        }

        public string Precompile(string template)
        {
            return _parsedScript.CallMethod("precompile", template).ToString();
        }
    }
}

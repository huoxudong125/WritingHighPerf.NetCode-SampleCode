using Microsoft.FxCop.Sdk;
using System.Reflection;

namespace FxCopRules
{
    public abstract class BaseCustomRule : BaseIntrospectionRule
    {
        // The manifest name is the default name space plus the name of the XML
        // rules file, without the extension.
        private const string ManifestName = "FxCopRules.Rules";

        // The assembly where the rule manifest is embedded (the current assembly in our case).
        private static readonly Assembly ResourceAssembly = typeof(BaseCustomRule).Assembly;

        protected BaseCustomRule(string ruleName)
            :base(ruleName, ManifestName, ResourceAssembly)
        {
        }
    }
}

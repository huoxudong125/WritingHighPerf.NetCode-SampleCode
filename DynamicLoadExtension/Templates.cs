using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoadExtension
{
    /// <summary>
    /// These are templates to use for IL generation and
    /// are not called directly.
    /// </summary>
    public class Templates
    {
        public static object CreateNewExtensionTemplate()
        {
            return new DynamicLoadExtension.Extension();
        }

        public static bool CallMethodTemplate(object ExtensionObj, string argument)
        {
            var extension = (DynamicLoadExtension.Extension)ExtensionObj;
            return extension.DoWork(argument);
        }
    }
}

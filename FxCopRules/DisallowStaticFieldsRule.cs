using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCopRules
{
    public class DisallowStaticFieldsRule : BaseCustomRule
    {
        public DisallowStaticFieldsRule()
            : base(typeof(DisallowStaticFieldsRule).Name)
        {
            
        }

        public override ProblemCollection Check(Member member)
        {
            var field = member as Field;
            if (field != null)
            {
                // Find all static data that isn't const or readonly
                if (field.IsStatic && !field.IsInitOnly && !field.IsLiteral)
                {
                    // field.FullName is an optional argument that will be used
                    // to format the Resolution string’s {0} parameter.
                    Resolution resolution = this.GetResolution(field.FullName);
                    Problem problem = new Problem(resolution, field.SourceContext);
                    this.Problems.Add(problem);
                }
            }
            
            return this.Problems;
        }       
    }
}

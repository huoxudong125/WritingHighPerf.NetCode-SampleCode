using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCopRules
{
    public class DisallowThreadCreationRule : BaseCustomRule
    {
        public DisallowThreadCreationRule() : base(typeof(DisallowThreadCreationRule).Name) { }

        public override ProblemCollection Check(Member member)
        {
            var method = member as Method;
            if (method != null)
            {
                VisitStatements(method.Body.Statements);
            }

            return base.Check(member);
        }

        public override void VisitConstruct(Construct construct)
        {
            if (construct != null)
            {
                var binding = construct.Constructor as MemberBinding;
                if (binding != null)
                {
                    var instanceInitializer = binding.BoundMember as InstanceInitializer;
                    if (instanceInitializer.DeclaringType.FullName == "System.Threading.Thread")
                    {
                        Problem problem = new Problem(this.GetResolution(), construct.SourceContext);
                        this.Problems.Add(problem);
                    }
                }
            }
            
            base.VisitConstruct(construct);
        }
    }
}

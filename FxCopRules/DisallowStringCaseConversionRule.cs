using Microsoft.FxCop.Sdk;

namespace FxCopRules
{
    public class DisallowStringCaseConversionRule : BaseCustomRule
    {
        public DisallowStringCaseConversionRule()
            : base(typeof(DisallowStringCaseConversionRule).Name)
        { }

        public override ProblemCollection Check(Member member)
        {
            var method = member as Method;
            if (method != null)
            {
                foreach (var instruction in method.Instructions)
                {
                    if (instruction.OpCode == OpCode.Call || instruction.OpCode == OpCode.Calli || instruction.OpCode == OpCode.Callvirt)
                    {
                        var targetMethod = instruction.Value as Method;
                        if (targetMethod.FullName == "System.String.ToUpper" 
                            || targetMethod.FullName == "System.String.ToLower")
                        {
                            Resolution resolution = this.GetResolution(method.FullName);
                            Problem problem = new Problem(resolution, method.SourceContext);
                            this.Problems.Add(problem);
                        }
                    }
                }
            }

            return this.Problems;
        }                
    }
}

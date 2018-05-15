using System;

namespace Footy.Features.General.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GivenAttribute : StepAttribute
    {
        public GivenAttribute(string regex)
            : base(regex)
        {
        }
    }
}
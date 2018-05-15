namespace Footy.Features.General.Steps
{
    public class WhenAttribute : StepAttribute
    {
        public WhenAttribute(string regex) : base(regex)
        {
        }
    }
}
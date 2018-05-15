namespace Footy.Features.General.Steps
{
    public class ThenAttribute : StepAttribute
    {
        public ThenAttribute(string regex) : base(regex)
        {
        }
    }
}
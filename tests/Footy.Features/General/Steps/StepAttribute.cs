using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Footy.Features.General.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StepAttribute : Attribute
    {
        public StepAttribute(string regex)
        {
            Regex = new Regex(regex);
        }

        public Regex Regex { get; }

        public bool IsMatch(string text)
        {
            return Regex.IsMatch(text);
        }

        public string[] GetParameters(string text)
        {
            var matches = Regex.Matches(text);
            return matches
                .SelectMany(m => m.Groups)
                .Skip(1)
                .Select(g => g.Value)
                .ToArray();
        }
    }
}
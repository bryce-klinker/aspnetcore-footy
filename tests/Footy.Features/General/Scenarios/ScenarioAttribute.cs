using System;
using System.Reflection;
using Footy.Features.General.Scenarios;
using Xunit.Sdk;

namespace Footy.Features.General.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ScenarioAttribute : BeforeAfterTestAttribute
    {
        public ScenarioAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override void Before(MethodInfo methodUnderTest)
        {
            Console.WriteLine($"Scenario: {Name}");
            base.Before(methodUnderTest);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            base.After(methodUnderTest);
            ScenarioContext.Current.Dispose();
        }
    }
}
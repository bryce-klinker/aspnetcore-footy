using System;
using System.Linq;
using System.Reflection;
using Footy.Features.General.Steps;

namespace Footy.Features.General
{
    public class StepDefinitionLocator
    {
        private static readonly Lazy<StepDefinitionLocator> Locator =
            new Lazy<StepDefinitionLocator>(() => new StepDefinitionLocator());

        private StepDefinition[] _steps;

        public static StepDefinitionLocator Instance => Locator.Value;

        public StepDefinition[] GetDefinitions()
        {
            if (_steps != null)
                return _steps;

            var definitions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetPublicTypes())
                .SelectMany(t => t.GetMethodsWithStepAttributes())
                .SelectMany(m =>
                    m.GetCustomAttributes<StepAttribute>().Select(a => new StepDefinition(m.DeclaringType, m, a)))
                .ToArray();

            return _steps = definitions;
        }
    }
}
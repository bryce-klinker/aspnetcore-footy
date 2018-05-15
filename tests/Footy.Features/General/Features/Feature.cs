using System;
using System.Linq;
using System.Threading.Tasks;
using Footy.Features.General.Steps;
using Xunit.Abstractions;

namespace Footy.Features.General.Features
{
    public class Feature
    {
        public Feature(ITestOutputHelper output)
        {
            Output = output;
            Steps = StepDefinitionLocator.Instance.GetDefinitions();
        }

        public StepDefinition[] Steps { get; }
        public ITestOutputHelper Output { get; }

        public async Task Given(string text)
        {
            Output.WriteLine($"Given {text}");
            await ExecuteStep<GivenAttribute>(text);
        }

        public async Task When(string text)
        {
            Output.WriteLine($"When {text}");
            await ExecuteStep<WhenAttribute>(text);
        }

        public async Task Then(string text)
        {
            Output.WriteLine($"Then {text}");
            await ExecuteStep<ThenAttribute>(text);
        }

        private async Task ExecuteStep<T>(string text)
        {
            var step = Steps.SingleOrDefault(s => s.IsMatch<T>(text))
                       ?? throw new InvalidOperationException($"No matchin step definition was found for: {text}");

            await step.Execute(text, Output);
        }
    }
}
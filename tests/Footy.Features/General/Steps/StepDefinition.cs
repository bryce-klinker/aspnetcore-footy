using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Footy.Features.General.Steps;
using Xunit.Abstractions;

namespace Footy.Features.General
{
    public class StepDefinition
    {
        public StepDefinition(Type type, MethodInfo method, StepAttribute attribute)
        {
            Type = type;
            Method = method;
            Attribute = attribute;
        }

        public Type Type { get; }
        public MethodInfo Method { get; }
        public StepAttribute Attribute { get; }

        public bool IsMatch<T>(string text)
        {
            return Attribute.GetType() == typeof(T)
                   && Attribute.IsMatch(text);
        }

        public async Task Execute(string text, ITestOutputHelper output)
        {
            var methodParameters = Method.GetParameters();
            var textParameters = Attribute.GetParameters(text);
            if (methodParameters.Length != textParameters.Length)
                throw new InvalidOperationException("Parameter count mismatch");

            var instance = CreateInstance(output);
            var convertedParameters = ConvertParameters(methodParameters, textParameters).ToArray();
            if (!Method.IsAsync())
                Method.Invoke(instance, convertedParameters);
            else
                await (Task) Method.Invoke(instance, convertedParameters);
        }

        private IEnumerable<object> ConvertParameters(ParameterInfo[] parameters, string[] values)
        {
            for (var i = 0; i < parameters.Length; i++)
                yield return Convert.ChangeType(values[i], parameters[0].ParameterType);
        }

        private object CreateInstance(ITestOutputHelper output)
        {
            var testOutputConstructor = Type
                .GetConstructor(new[] {typeof(ITestOutputHelper)});
            return testOutputConstructor != null
                ? testOutputConstructor.Invoke(new object[] {output})
                : Activator.CreateInstance(Type);
        }
    }
}
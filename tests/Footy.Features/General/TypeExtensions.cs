using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Footy.Features.General.Steps;

namespace Footy.Features.General
{
    public static class TypeExtensions
    {
        public static IEnumerable<MethodInfo> GetMethodsWithStepAttributes(this Type type)
        {
            return type.GetMethods()
                .Where(m => m.GetCustomAttribute<StepAttribute>() != null);
        }
    }
}
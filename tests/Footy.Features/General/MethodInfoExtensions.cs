using System.Reflection;
using System.Threading.Tasks;

namespace Footy.Features.General
{
    public static class MethodInfoExtensions
    {
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task)
                   || method.ReturnType.IsGenericType &&
                   method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }
    }
}
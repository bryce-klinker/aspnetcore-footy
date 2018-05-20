using Microsoft.AspNetCore.Mvc;

namespace Footy.TestUtilities
{
    public static class OkObjectResultExtensions
    {
        public static T Value<T>(this OkObjectResult result)
        {
            return (T)result.Value;
        }
    }
}
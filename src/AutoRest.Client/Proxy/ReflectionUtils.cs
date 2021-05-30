using System.Linq;

namespace AutoRest.Client.Proxy
{
    public static class ReflectionUtils
    {
        public static object InvokeMethod(object target, string methodName, params object[] methodParams)
        {
            var method = target.GetType().GetMethod(methodName, 0, methodParams
                .Select(x => x.GetType()).ToArray());

            return method.Invoke(target, methodParams);
        }
    }
}
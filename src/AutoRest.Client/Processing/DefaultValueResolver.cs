using System;

namespace AutoRest.Client.Processing
{
    public class DefaultValueResolver: IValueResolver
    {
        public object Resolve(Type objectType)
        {
            return Activator.CreateInstance(objectType);
        }
    }
}
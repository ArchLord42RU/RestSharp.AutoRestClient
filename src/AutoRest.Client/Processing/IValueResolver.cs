using System;

namespace AutoRest.Client.Processing
{
    public interface IValueResolver
    {
        object Resolve(Type objectType);
    }
}
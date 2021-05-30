using System;
using System.Reflection;
using AutoRest.Client.Processing.Response;
using RestSharp;

namespace AutoRest.Client.Attributes.Response
{
    public class FromBodyAttribute: ResponseParameterBindingAttribute
    {
        public override void Bind(ResponseParameterBindingContext context)
        {
            var genericMethod = context.Deserializer
                                    .GetType()
                                    .GetMethod(nameof(IResponseDeserializer.Deserialize), BindingFlags.Public | BindingFlags.Instance)
                                    ?.MakeGenericMethod(context.ReturnValueType) ?? throw new InvalidOperationException();

            var deserializedResponse = genericMethod.Invoke(context.Deserializer, new object[]{context.Response});

            var data = deserializedResponse.GetType().GetProperty(nameof(IRestResponse<object>.Data))
                ?.GetValue(deserializedResponse);            
            
            context.ReturnValue = data;
        }
    }
}
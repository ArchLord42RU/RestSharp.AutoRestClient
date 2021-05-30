#nullable enable
using System;
using RestSharp;

namespace AutoRest.Client.Processing.Response
{
    public class ResponseParameterBindingContext
    {
        public IRestResponse? Response { get; }
        
        public IResponseDeserializer Deserializer { get; }

        public Type ReturnValueType { get; }
        
        public object? ReturnValue { get; set; }
        
        public ResponseParameterBindingContext(IRestResponse? response, IResponseDeserializer deserializer,
            Type returnValueType, object? returnValue)
        {
            Response = response;
            Deserializer = deserializer;
            ReturnValueType = returnValueType;
            ReturnValue = returnValue;
        }
    }
}
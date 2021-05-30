using System;
using System.Reflection;

namespace AutoRest.Client.Processing.Requests
{
    public class RequestParameterBindingContext
    {
        public RequestExecutionContext ExecutionContext { get; internal set; }
        
        public Type MemberType { get; internal set; }
        
        public string MemberName { get; internal set; }
        
        public object MemberValue { get; internal set; }
        
        public ICustomAttributeProvider MemberAttributes { get; internal set; }
    }
}
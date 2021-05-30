using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoRest.Client.Attributes.Requests;
using AutoRest.Client.Attributes.Response;
using AutoRest.Client.Processing.Requests;
using AutoRest.Client.Processing.Response;

namespace AutoRest.Client.Processing
{
    public static class ProcessingUtils
    {
        public static async Task ApplyRequestModifiersAttributes(ICustomAttributeProvider attributeProvider, RequestExecutionContext context)
        {
            var attributes = attributeProvider.GetCustomAttributes(true).OfType<RequestModifierAttribute>();
            
            foreach (var attribute in attributes)
            {
                var attributeType = attribute.GetType();
                
                var methodInfo = attributeType.GetMethod(nameof(RequestModifierAttribute.ApplyAsync), 
                    BindingFlags.Public | BindingFlags.Instance);
        
                if (methodInfo == null)
                    throw new InvalidOperationException("Attribute does not inherited from IAsyncRequestModifier");
            
                var methodHasOverride = methodInfo.DeclaringType == attributeType;
            
                if (methodHasOverride)
                    await attribute.ApplyAsync(context);
                else
                    // ReSharper disable once MethodHasAsyncOverload
                    attribute.Apply(context);
            }
        }

        public static async Task ApplyRequestParameterBindingAttributes(ICustomAttributeProvider attributeProvider,
            RequestParameterBindingContext context)
        {
            var attributes = attributeProvider.GetCustomAttributes(true).OfType<RequestParameterBindingAttribute>();
            
            foreach (var attribute in attributes)
            {
                var attributeType = attribute.GetType();
                
                var methodInfo = attributeType.GetMethod(nameof(RequestParameterBindingAttribute.BindAsync),
                    BindingFlags.Public | BindingFlags.Instance);
        
                if (methodInfo == null)
                    throw new InvalidOperationException("Attribute does not inherited from IAsyncRequestParameterBinder");
            
                var methodHasOverride = methodInfo.DeclaringType == attributeType;
            
                if (methodHasOverride)
                    await attribute.BindAsync(context);
                else
                    // ReSharper disable once MethodHasAsyncOverload
                    attribute.Bind(context);
            }
        }

        public static async Task ApplyResponseParameterBindingAttributes(ICustomAttributeProvider attributeProvider,
            ResponseParameterBindingContext context)
        {
            var attributes = attributeProvider.GetCustomAttributes(true)
                .OfType<ResponseParameterBindingAttribute>().ToList();
            
            if (!attributes.Any())
                attributes.Add(new FromBodyAttribute());
            
            foreach (var attribute in attributes)
            {
                var attributeType = attribute.GetType();
                
                var methodInfo = attributeType.GetMethod(nameof(ResponseParameterBindingAttribute.BindAsync),
                    BindingFlags.Public | BindingFlags.Instance);
        
                if (methodInfo == null)
                    throw new InvalidOperationException("Attribute does not inherited from IAsyncResponseParameterBinder");
            
                var methodHasOverride = methodInfo.DeclaringType == attributeType;
            
                if (methodHasOverride)
                    await attribute.BindAsync(context);
                else
                    // ReSharper disable once MethodHasAsyncOverload
                    attribute.Bind(context);
            }
        }

        public static string CombineUrl(string left, string right)
        {
            return Path.Join(left, right);
        }
    }
}
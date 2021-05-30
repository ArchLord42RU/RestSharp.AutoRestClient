using System;
using System.Threading.Tasks;
using AutoRest.Client.Examples.Microsoft;

namespace AutoRest.Client.Examples
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Running simple example");
            await RequestsExample.SimpleUsage();

            Console.WriteLine("Running simple example (only header)");
            await RequestsExample.SimpleUsageOnlyHeader();
            
            Console.WriteLine("Running advanced example");
            await RequestsExample.AdvancedMapping();
            
            Console.WriteLine("Running MS DI example");
            await MicrosoftDiExample.Run();
            
            Console.WriteLine();
        }
    }
}
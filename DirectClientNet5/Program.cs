using System;
using System.Threading.Tasks;
using TestComHostNet5;

namespace DirectClientNet5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new BogusComHost();
            Action<string> logAction = (message) => Console.WriteLine(message);
            await host.Test(logAction); // correctly finds and runs one test
        }
    }
}
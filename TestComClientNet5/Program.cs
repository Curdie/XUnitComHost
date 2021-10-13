using System;
using System.Threading.Tasks;

namespace TestComClientNet5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            dynamic myComObject = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("C3287415-77D3-48A7-B950-B11AA08B2263")));
            Action<string> logAction = (message) => Console.WriteLine(message);
            await myComObject.Test(logAction);  // should discover and run 1 test, but finds 0
        }
    }
}

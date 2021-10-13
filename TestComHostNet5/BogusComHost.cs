using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TestComHostNet5
{
    // you must manually execute "regsrv32 BogusComHost.comhost.dll" as admin to register for COM interop
    [ComVisible(true)]
    [Guid("C3287415-77D3-48A7-B950-B11AA08B2263")]
    public class BogusComHost
    {
        //private const string _assemblyPath2 = @"C:\Users\jon\source\repos\SWCustomPropertyTest\TestProject1\bin\Debug\net5.0\TestProject1.dll";
        private const string _assemblyPath2 = @"..\..\..\..\\TestProject1\bin\Debug\net5.0\TestProject1.dll";

        public async Task Test(Action<string> logAction)
        {
            var runner = new SillyTestRunner(_assemblyPath2, logAction);
            await runner.RunTestsAsync();
        }
    }
}

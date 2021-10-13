using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit.Runners;

namespace TestComHostNet5
{
    public class SillyTestRunner
    {
        private readonly Action<string> log;
        private readonly string assemblyPath;
        public SillyTestRunner(string assemblyPath, Action<string> log)
        {
            this.assemblyPath = assemblyPath;
            this.log = log;
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }
        public async Task RunTestsAsync()
        {
            using var runner = AssemblyRunner.WithoutAppDomain(assemblyPath);
            log("Running tests");
            var tcs = new TaskCompletionSource<object>();
            runner.OnDiscoveryComplete = info => log($"{Environment.Version} Discovered {info.TestCasesDiscovered} test cases.");
            runner.OnExecutionComplete = info => tcs.TrySetResult(null);
            runner.OnTestFailed = info => log($"Failed: {info.MethodName}");
            runner.OnTestSkipped = info => log($"Skipped: {info.MethodName}");
            runner.OnTestPassed = info => log($"Passed: {info.MethodName}");
            runner.OnDiagnosticMessage = info => log($"{info.Message}");
            runner.Start(diagnosticMessages: true, internalDiagnosticMessages: true);
            await tcs.Task;
            log("Done running tests.");
            while (runner.Status != AssemblyRunnerStatus.Idle)
            {
                await Task.Delay(100);
            }
        }

        public Assembly ResolveAssembly(object? s, ResolveEventArgs e)
        {
            var dir = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            var asmName = new AssemblyName(e.Name);
            var asmPath = System.IO.Path.Combine(dir, asmName.Name + ".dll");
            if (!System.IO.File.Exists(asmPath))
            {
                asmPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assemblyPath), asmName.Name + ".dll");
            }
            try
            {
                log($"Loading DLL {asmPath}");
                return Assembly.LoadFrom(asmPath);
            }
            catch
            {
                return null;
            }
        }
    }
}

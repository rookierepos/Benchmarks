using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace Benchmarks.Infrastructure
{
    public class MultipleRuntimes : ManualConfig
    {
        public MultipleRuntimes()
        {
            // Add(Job.Default.With(
            //     CsProjCoreToolchain.From(
            //         new NetCoreAppSettings(
            //             targetFrameworkMoniker: "netcoreapp2.1",
            //             runtimeFrameworkVersion: "2.1.9",
            //             name: ".NET Core 2.1"))));
            // Add(Job.Default.With(
            //     CsProjCoreToolchain.From(
            //         new NetCoreAppSettings(
            //             targetFrameworkMoniker: "netcoreapp2.2",
            //             runtimeFrameworkVersion: "2.2.3",
            //             name: ".NET Core 2.2"))));
            Add(Job.Default.With(
                CsProjCoreToolchain.From(
                    new NetCoreAppSettings(
                        targetFrameworkMoniker: "netcoreapp3.1",
                        runtimeFrameworkVersion: "3.1.1",
                        name: ".NET Core 3.1"))));
        }
    }
}

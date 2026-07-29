using System.Runtime.InteropServices;
using BlazorCodeChallenge.Contracts.Diagnostics;

namespace BlazorCodeChallenge.Core.Diagnostics;

public sealed class RuntimeDiagnosticsService : IRuntimeDiagnosticsService
{
    public RuntimeDiagnosticsResponse GetDiagnostics(
        string applicationName,
        string environmentName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationName);
        ArgumentException.ThrowIfNullOrWhiteSpace(environmentName);

        return new RuntimeDiagnosticsResponse
        {
            ApplicationName = applicationName,
            EnvironmentName = environmentName,
            FrameworkDescription = RuntimeInformation.FrameworkDescription,
            OperatingSystemDescription = RuntimeInformation.OSDescription,
            ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
            MachineArchitecture = RuntimeInformation.OSArchitecture.ToString(),
            RuntimeVersion = Environment.Version.ToString(),
            TimestampUtc = DateTimeOffset.UtcNow
        };
    }
}

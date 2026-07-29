using BlazorCodeChallenge.Contracts.Diagnostics;

namespace BlazorCodeChallenge.Core.Diagnostics;

public interface IRuntimeDiagnosticsService
{
    RuntimeDiagnosticsResponse GetDiagnostics(string applicationName, string environmentName);
}

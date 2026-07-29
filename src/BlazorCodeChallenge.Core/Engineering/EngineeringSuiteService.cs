using BlazorCodeChallenge.Contracts.Engineering;

namespace BlazorCodeChallenge.Core.Engineering;

public sealed class EngineeringSuiteService : IEngineeringSuiteService
{
    public EngineeringSuiteStatusResponse GetStatus()
    {
        return new EngineeringSuiteStatusResponse(
            Name: "BlazorExpo Engineering Suite",
            Version: "1.0",
            Status: "Operational",
            Capabilities:
            [
                "Blazor WebAssembly",
                "ASP.NET Core Web API",
                "Shared Contracts",
                "Core Application Services"
            ]);
    }
}

namespace BlazorCodeChallenge.Contracts.Diagnostics;

public sealed record RuntimeDiagnosticsResponse
{
    public required string ApplicationName { get; init; }

    public required string EnvironmentName { get; init; }

    public required string FrameworkDescription { get; init; }

    public required string OperatingSystemDescription { get; init; }

    public required string ProcessArchitecture { get; init; }

    public required string MachineArchitecture { get; init; }

    public required string RuntimeVersion { get; init; }

    public DateTimeOffset TimestampUtc { get; init; }
}

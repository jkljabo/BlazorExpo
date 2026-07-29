namespace BlazorCodeChallenge.Contracts.Engineering;

public sealed record EngineeringSuiteStatusResponse(
    string Name,
    string Version,
    string Status,
    IReadOnlyCollection<string> Capabilities);

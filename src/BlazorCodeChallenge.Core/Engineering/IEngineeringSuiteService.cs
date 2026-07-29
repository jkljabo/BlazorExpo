using BlazorCodeChallenge.Contracts.Engineering;

namespace BlazorCodeChallenge.Core.Engineering;

public interface IEngineeringSuiteService
{
    EngineeringSuiteStatusResponse GetStatus();
}

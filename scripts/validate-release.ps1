$ErrorActionPreference = "Stop"

$project = "BlazorCodeChallenge.csproj"
$publishDirectory = "bin\Release\net8.0\publish\wwwroot"

function Invoke-Step {
    param (
        [string]$Name,
        [scriptblock]$Command
    )

    Write-Host ""
    Write-Host "=== $Name ==="

    & $Command

    if ($LASTEXITCODE -ne 0) {
        throw "$Name failed with exit code $LASTEXITCODE."
    }
}

Write-Host "BlazorExpo Release Validation"
Write-Host "============================="

# Verify that validation is being performed from develop.
$branch = git branch --show-current

if ($LASTEXITCODE -ne 0) {
    throw "Unable to determine the current Git branch."
}

if ($branch -ne "develop") {
    throw "Release validation must be performed from develop. Current branch: $branch"
}

Write-Host "Branch: $branch"

# Verify required WebAssembly tooling.
$workloads = dotnet workload list

if ($LASTEXITCODE -ne 0) {
    throw "Unable to determine installed .NET workloads."
}

$wasmToolsInstalled = $workloads |
    Select-String -Pattern '^\s*wasm-tools\s'

if (-not $wasmToolsInstalled) {
    throw "Required .NET workload 'wasm-tools' is not installed."
}

Write-Host "wasm-tools: installed"

Invoke-Step "Clean Release build" {
    dotnet clean $project -c Release
}

Invoke-Step "Restore dependencies" {
    dotnet restore $project
}

Invoke-Step "Build Release" {
    dotnet build $project -c Release --no-restore
}

Invoke-Step "Publish Release" {
    dotnet publish $project -c Release --no-restore
}

Invoke-Step "Check Git whitespace" {
    git diff --check
}

if (-not (Test-Path $publishDirectory)) {
    throw "Expected publish directory was not produced: $publishDirectory"
}

Write-Host ""
Write-Host "Publish output verified: $publishDirectory"

Write-Host ""
Write-Host "================================"
Write-Host "RELEASE VALIDATION PASSED"
Write-Host "================================"

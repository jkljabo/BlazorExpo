# BlazorExpo Operations Runbook

**Project:** BlazorExpo

**Document Version:** 1.1

**Release Baseline:** RC1 (Sprint 1)

**Current Development:** Sprint 2

**Last Updated:** July 2026

**Owner:** Jason Little

## Purpose

This runbook documents the verified development, build, deployment, validation, and operational procedures for BlazorExpo. It serves as the authoritative guide for building, validating, promoting, deploying, and troubleshooting the application using GitHub and Netlify.

The runbook preserves the Release Candidate 1 (RC1) operational baseline established during Sprint 1 while incorporating process improvements introduced during Sprint 2.

## Scope

Sprint 1 established the initial production deployment pipeline and verified the RC1 release process.

Sprint 2 builds upon that baseline by introducing:

- Clean Release builds with zero warnings and zero errors
- Integration and evaluation of the `wasm-tools` workload in the Blazor WebAssembly publishing process
- Separation of active development from the production branch
- Controlled promotion from `develop` to `main`
- Reduced unnecessary Netlify deployments
- Disabled Netlify Branch Deploys and Deploy Previews
- Local validation before production promotion

The current release workflow is:

```text
feature/*
    │
    ▼
develop
    │
    ▼
Local Release Build
    │
    ▼
Local Release Publish
    │
    ▼
Validation
    │
    ▼
main
    │
    ▼
Netlify Production Deployment
    │
    ▼
Production Smoke Test
```

The `develop` branch is the integration branch for active Sprint development. The `main` branch represents production-ready code and is the only branch intended to trigger a Netlify production deployment.

## Phase 1 - Local Verification

All development changes must pass appropriate local verification before they are considered eligible for integration or production promotion.

Feature-branch development may use the individual validation gates documented in this phase. After changes have been integrated into `develop`, the automated Release validation script provides the standard release-candidate validation workflow.

### Automated Release Validation

The standard local Release validation workflow is automated by:

```powershell
.\scripts\validate-release.ps1
```

The validation script performs the primary local release gates in a consistent sequence:

- Verifies that validation is being performed from the `develop` branch.
- Verifies that the required `wasm-tools` workload is installed.
- Cleans the Release build output.
- Restores project dependencies.
- Performs a Release build.
- Performs a Release publish.
- Runs `git diff --check`.
- Verifies that the expected publish directory was produced.

A successful validation concludes with:

```text
RELEASE VALIDATION PASSED
```

The automated script is the standard validation entry point when preparing a release candidate on `develop`.

The individual procedures below document the underlying validation gates and remain available for manual verification, troubleshooting, and diagnostic use.

### 1. Verify the Current Branch

Active Sprint development should normally occur on `develop` or a `feature/*` branch.

```powershell
git branch --show-current
git status
```

Expected results:

- Development work is not being performed directly on `main`.
- The working tree contains only expected changes.
- The local branch is synchronized with its intended remote branch before release preparation.

### 2. Clean the Project

```powershell
dotnet clean BlazorCodeChallenge.csproj -c Release
```

### 3. Restore Dependencies

```powershell
dotnet restore BlazorCodeChallenge.csproj
```

### 4. Perform a Release Build

```powershell
dotnet build BlazorCodeChallenge.csproj -c Release
```

Required result:

```text
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

A Release build containing compiler warnings or errors does not satisfy the Sprint 2 quality gate.

### 5. Perform a Release Publish

```powershell
dotnet publish BlazorCodeChallenge.csproj -c Release
```

Expected publish output:

```text
bin/Release/net8.0/publish/
```

The published Blazor WebAssembly application is located under:

```text
bin/Release/net8.0/publish/wwwroot
```

### 6. Verify Git State

```powershell
git status
git diff --check
```

Before committing:

- Review all modified files.
- Confirm no unintended changes are present.
- Confirm `git diff --check` reports no whitespace errors.
- Confirm no secrets or local configuration files are being committed.

### 7. Commit and Push Development Work

Validated Sprint work is committed and pushed to `develop` or the appropriate `feature/*` branch.

Example:

```powershell
git add <files>
git commit -m "<type>: <description>"
git push
```

Pushing development branches does not constitute a production release and must not trigger a Netlify production deployment.

Production promotion to `main` occurs only after the release validation process is complete.

## Phase 2 - Repository Verification (GitHub)

GitHub is the authoritative source for both active development and production release history.

The repository uses the following branch roles:

- `develop` — integration branch for active Sprint development.
- `feature/*` — optional branches for isolated development work.
- `main` — production branch containing only production-ready code.

### 1. Verify Development Synchronization

Before release preparation, verify that the validated development work has been committed and pushed.

```powershell
git status
git branch -vv
```

Expected results:

- The working tree is clean.
- `develop` is synchronized with `origin/develop`.
- All intended Sprint changes are committed.
- No required work exists only on the local machine.

### 2. Verify the Development Commit

Confirm the expected commits are present locally and on GitHub.

```powershell
git log --oneline --decorate -5
```

Verify:

- The expected Sprint commits are present on `develop`.
- `origin/develop` references the expected development state.
- `main` has not been modified as part of normal development.

### 3. Verify Release Configuration

Before promoting a release candidate, review the deployment-sensitive configuration:

- `netlify.toml`
- `global.json`
- `netlify/edge-functions/tmdb.js`
- `Program.cs`
- `BlazorCodeChallenge.csproj`
- `TMDBService.cs`

Confirm that:

- Configuration matches the intended release.
- No secrets are committed.
- The Netlify production branch remains `main`.
- Branch Deploys remain disabled.
- Deploy Previews remain disabled.

### 4. Production Promotion Rule

A push to `develop` or `feature/*` is a development operation and must not trigger a production deployment.

Promotion to `main` is an intentional release operation performed only after:

- Local Release build succeeds with zero warnings and zero errors.
- Local Release publish succeeds.
- Functional validation succeeds.
- Git state and release configuration have been reviewed.
- The release candidate is approved for production deployment.

The production flow is:

```text
develop
    │
    ▼
Release Validation
    │
    ▼
main
    │
    ▼
Netlify Production Deployment
```

Because pushes to `main` trigger production deployment, routine development work must not be performed directly on `main`.

### 5. Deployment Resource Management

Netlify production deployments are treated as controlled release resources rather than routine development validation.

Development work should be built, published, and tested locally whenever possible.

Pushes to `main` should be reserved for validated production release candidates. This reduces unnecessary Netlify builds and preserves available deployment resources.

### Verification Result

Repository verification is complete when:

- Development work is safely committed and pushed to GitHub.
- `develop` represents the validated development state.
- `main` remains at the current production state until intentional promotion.
- Deployment-sensitive configuration has been reviewed.
- No unintended production deployment has been triggered.
		
## Phase 2.5 – Netlify Compliance

Netlify configuration is treated as production infrastructure configuration and must remain synchronized with the production release strategy.

### netlify.toml

**Status:** Verified

The project uses `netlify.toml` as the single source of truth for repository-controlled Netlify configuration.

The Sprint 2 deployment configuration includes:

- Release publishing of the Blazor WebAssembly application.
- Installation of the `wasm-tools` workload before publishing.
- `--skip-manifest-update` to avoid unnecessary workload manifest changes during the Netlify build.
- The production publish directory.
- TMDB Edge Function routing.
- SPA fallback routing.

The current build command is:

```text
dotnet workload install wasm-tools --skip-manifest-update && dotnet publish BlazorCodeChallenge.csproj -c Release
```

The production publish directory is:

```text
bin/Release/net8.0/publish/wwwroot
```

The TMDB Edge Function is mapped through `netlify.toml`:

```toml
[[edge_functions]]
function = "tmdb"
path = "/tmdb/*"
```

Blazor client-side routing is supported by the SPA fallback:

```toml
[[redirects]]
from = "/*"
to = "/index.html"
status = 200
```

The legacy `wwwroot/_redirects` file has been removed so that routing configuration is maintained in one location.

### WebAssembly Workload Strategy

Blazor WebAssembly Release publishing can use the `wasm-tools` workload for the WebAssembly publishing toolchain.

Local development already has the required workload installed.

Netlify's build environment is ephemeral and therefore installs the workload as part of the production build command.

The `--skip-manifest-update` option is used to reduce unnecessary workload manifest activity while installing the workload required by the build.

The workload strategy is therefore:

```text
Local Development
    │
    └── wasm-tools installed locally

Netlify Build
    │
    └── install wasm-tools --skip-manifest-update
            │
            ▼
       Release Publish
```

A successful local publish does not eliminate the need for the Netlify workload installation because the Netlify build runs in an independent environment.

### global.json

**Status:** Verified

The project targets .NET 8.

The repository currently establishes the deployment SDK baseline through `global.json` while allowing compatible newer SDKs to be used locally through SDK roll-forward.

Current strategy:

```json
{
  "sdk": {
    "version": "8.0.128",
    "rollForward": "latestFeature"
  }
}
```

This allows:

- Netlify to use the supported .NET 8 SDK baseline.
- Local development to use a compatible newer .NET 8 SDK.
- The project to remain on the .NET 8 target framework.
- SDK selection to remain explicit and reproducible.

Changes to `global.json` must be validated locally and against the Netlify build environment before production promotion.

### tmdb.js

**Status:** Verified

The TMDB Edge Function provides the server-side boundary between the Blazor WebAssembly client and TMDB.

The Edge Function has been verified to:

- Read TMDB configuration from Netlify environment variables.
- Keep the TMDB API key outside the browser application.
- Proxy application requests to the TMDB API.
- Bundle successfully during Netlify deployment.
- Serve Movie Time requests correctly.

The browser must never receive the TMDB API key.

### Deployment Scope Controls

Netlify deployment behavior is intentionally restricted to conserve deployment resources and maintain a controlled production release process.

Current deployment policy:

- Production branch: `main`
- Branch Deploys: disabled
- Deploy Previews: disabled
- Active development branch: `develop`
- Optional development branches: `feature/*`

Development pushes must not be used as Netlify validation builds.

Netlify production deployment is reserved for validated release candidates promoted to `main`.

### Compliance Result

Netlify compliance is satisfied when:

- `netlify.toml` contains the expected production configuration.
- `global.json` reflects the approved .NET SDK strategy.
- `wasm-tools` is incorporated into the Netlify production build process.
- TMDB credentials remain server-side.
- Edge Function routing is correctly configured.
- SPA fallback routing is correctly configured.
- Branch Deploys remain disabled.
- Deploy Previews remain disabled.
- Only intentional promotion to `main` can initiate a production deployment.
		
## Phase 3 – Netlify Project

The Netlify project provides the production hosting and deployment environment for BlazorExpo.

### Production Project Configuration

The verified production configuration is:

- **Provider:** GitHub
- **Repository:** BlazorExpo
- **Team:** Jabo's BlazorExpo
- **Site Name:** jasonlittle-dev
- **Production Branch:** `main`
- **Branch Deploys:** Disabled
- **Deploy Previews:** Disabled

The `main` branch represents the production release state.

Changes pushed to `develop` or `feature/*` branches must not trigger production deployments.

### Production Deployment Boundary

A Netlify production deployment occurs only after a validated release candidate is intentionally promoted to `main`.

```text
develop
    │
    ▼
Local Release Validation
    │
    ▼
Release Approval
    │
    ▼
main
    │
    ▼
Netlify Production Build
    │
    ▼
Production Deployment
```

Netlify must not be used as the primary build-validation environment during normal development.

Local builds, publishes, and functional testing should identify problems before production promotion.

### Deployment Resource Policy

Netlify build and deployment resources are treated as limited production resources.

To reduce unnecessary resource consumption:

- Routine development occurs on `develop` or `feature/*`.
- Development branches are validated locally.
- Branch Deploys remain disabled.
- Deploy Previews remain disabled.
- Production deployments are reserved for validated release candidates.
- Multiple small production deployments should be avoided when changes can be grouped into a validated release.

This policy allows GitHub to remain the remote source of truth for development work without coupling every push to a Netlify deployment.

### Verification Result

The Netlify project configuration is considered valid when:

- GitHub remains the connected repository provider.
- BlazorExpo remains the connected repository.
- `main` remains the production branch.
- Branch Deploys are disabled.
- Deploy Previews are disabled.
- Development pushes do not initiate production deployments.
- Production deployment occurs only through intentional promotion to `main`.

## Phase 4 - Build Settings

Production build configuration is maintained primarily in `netlify.toml`.

The Netlify project settings and repository configuration must remain consistent with that file.

### Current Production Build Configuration

**Base Directory**

```text
(blank)
```

**Build Command**

```text
dotnet workload install wasm-tools --skip-manifest-update && dotnet publish BlazorCodeChallenge.csproj -c Release
```

**Publish Directory**

```text
bin/Release/net8.0/publish/wwwroot
```

**Functions Directory**

```text
(blank)
```

No Netlify Functions directory is currently required.

**Edge Functions Directory**

```text
netlify/edge-functions
```

This is the default project location used for the TMDB Edge Function.

### .NET SDK Configuration

The Netlify build environment uses the repository's approved .NET 8 SDK strategy.

`global.json` establishes the SDK baseline:

```json
{
  "sdk": {
    "version": "8.0.128",
    "rollForward": "latestFeature"
  }
}
```

The Netlify environment also specifies:

```text
DOTNET_VERSION = 8.0.128
```

The SDK configuration must remain compatible with the project's `net8.0` target framework.

### WebAssembly Build Tooling

The production build explicitly installs the `wasm-tools` workload before publishing:

```text
dotnet workload install wasm-tools --skip-manifest-update
```

The publish operation then runs:

```text
dotnet publish BlazorCodeChallenge.csproj -c Release
```

Together, these commands form the production build command defined in `netlify.toml`.

The workload installation is performed within the Netlify build environment and does not depend on the workload installed on the developer workstation.

### Build Output

A successful production publish produces the deployable Blazor WebAssembly application under:

```text
bin/Release/net8.0/publish/wwwroot
```

Netlify publishes this directory as the static production site.

### Edge Function Packaging

During the production build, Netlify must detect and package:

```text
netlify/edge-functions/tmdb.js
```

The deployment log should confirm that the `tmdb` Edge Function was bundled successfully.

### Build Validation

A successful production build should confirm:

- The expected .NET SDK is available.
- Project dependencies restore successfully.
- `wasm-tools` installation completes successfully.
- The Blazor project builds successfully.
- Release publishing completes successfully.
- The expected publish directory exists.
- The TMDB Edge Function is detected and bundled.
- Netlify initiates deployment from the correct publish directory.

### Important WebAssembly Optimization Note

During Sprint 2 verification, Netlify successfully installed the `wasm-tools` workload but the subsequent publish still reported:

```text
Publishing without optimizations. Although it's optional for Blazor, we strongly recommend using `wasm-tools` workload!
```

The application nevertheless built, deployed, and passed production smoke testing.

Therefore:

- `wasm-tools` installation is part of the approved build configuration.
- The remaining Netlify optimization message is currently non-blocking.
- The runbook does not claim that Netlify WebAssembly optimization has been conclusively verified.
- Further investigation may be performed later without blocking normal application development.

### Build Configuration Result

The production build configuration is considered valid when:

- `netlify.toml` contains the approved build command.
- The publish directory is correct.
- The .NET SDK strategy remains compatible with Netlify.
- `wasm-tools` installation succeeds.
- The application publishes successfully.
- The Edge Function bundles successfully.
- Netlify deploys the expected `wwwroot` output.
	
## Phase 5 - Environment Variables

BlazorExpo uses Netlify environment variables to provide server-side configuration required by the TMDB Edge Function.

Sensitive configuration must never be stored in the Blazor WebAssembly client, committed to GitHub, or included in the published `wwwroot` output.

### Required Environment Variables

The production environment requires the following variables.

#### API_KEY

`API_KEY` contains the TMDB API credential used by the Netlify Edge Function.

Requirements:

- Stored only in the Netlify environment.
- Never committed to GitHub.
- Never placed in client-side configuration.
- Never returned to the browser.
- Available to the `tmdb` Edge Function during execution.

The actual credential value must not be recorded in this runbook.

#### API_URL

The TMDB API base URL is:

```text
https://api.themoviedb.org/3
```

Verify that:

- The variable name is `API_URL`.
- The value contains no leading or trailing whitespace.
- The configured value matches the URL expected by the Edge Function.
- No unnecessary trailing slash is introduced.

### Security Boundary

The expected request flow is:

```text
Blazor WebAssembly Client
        │
        ▼
/tmdb/*
        │
        ▼
Netlify Edge Function
        │
        ├── API_KEY
        ├── API_URL
        │
        ▼
TMDB API
```

The browser communicates with the application-owned `/tmdb/*` endpoint rather than communicating with TMDB using a client-exposed API credential.

The Netlify Edge Function is responsible for reading the environment variables and constructing the authenticated TMDB request.

### Security Requirements

The following requirements apply to production configuration:

- TMDB credentials remain exclusively in the Netlify server-side environment.
- Secrets must never be committed to source control.
- Secrets must never be embedded in Blazor WebAssembly assemblies or static assets.
- Secrets must never be written to application logs.
- The browser must never receive the TMDB API key.
- Client-side requests to TMDB functionality must pass through the Edge Function.

### Pre-Deployment Verification

Before an intentional production deployment, verify that:

- `API_KEY` exists in the Netlify production environment.
- `API_URL` exists and contains the expected TMDB base URL.
- The Edge Function continues to reference environment configuration rather than hard-coded credentials.
- No secrets appear in pending Git changes.

The repository can be inspected before release with:

```powershell
git status
git diff
git diff --cached
```

Credential values must not be printed merely for deployment verification.

### Post-Deployment Verification

After production deployment:

1. Open Movie Time.
2. Confirm that live TMDB data loads successfully.
3. Exercise a request that passes through the `/tmdb/*` Edge Function.
4. Confirm that the browser console contains no authentication or Edge Function errors.
5. Confirm through browser developer tools that the TMDB API key is not exposed in client requests or static application resources.

Successful Movie Time operation verifies that the application can use the configured server-side credentials without exposing those credentials to the client.

### Environment Configuration Result

Environment configuration is considered valid when:

- Required Netlify environment variables exist.
- Movie Time successfully retrieves live TMDB data through the Edge Function.
- No TMDB credential is stored in the repository.
- No TMDB credential is exposed to the browser.
- Production requests follow the intended client-to-proxy-to-TMDB architecture.
	
## Phase 6 - Configuration Files

Before a release is promoted to `main`, verify that the deployment-critical configuration files represent the intended production baseline.

These files should be reviewed from the validated release candidate on `develop` before production promotion.

### netlify.toml

`netlify.toml` is the authoritative Netlify build and routing configuration.

Verify that it contains the expected:

- Release build command
- `wasm-tools` installation command
- Publish directory
- .NET SDK environment configuration
- TMDB Edge Function mapping
- SPA fallback redirect

Current production baseline:

```toml
[build]
command = "dotnet workload install wasm-tools --skip-manifest-update && dotnet publish BlazorCodeChallenge.csproj -c Release"
publish = "bin/Release/net8.0/publish/wwwroot"

[build.environment]
DOTNET_VERSION = "8.0.128"

[[edge_functions]]
function = "tmdb"
path = "/tmdb/*"

[[redirects]]
from = "/*"
to = "/index.html"
status = 200
```
	
The legacy `wwwroot/_redirects` file must not be reintroduced unless the routing strategy is intentionally changed and documented.

### global.json

Verify that `global.json` continues to represent the approved .NET SDK strategy.

Current baseline:

```json
{
  "sdk": {
    "version": "8.0.128",
    "rollForward": "latestFeature"
  }
}
```

This configuration establishes the deployment-compatible SDK baseline while allowing compatible newer .NET 8 feature-band SDKs during local development.

### BlazorCodeChallenge.csproj

Verify that the project file continues to target the intended framework and package baseline.

At minimum, confirm:

- `TargetFramework` remains `net8.0`.
- Nullable reference types remain enabled.
- Implicit usings remain enabled.
- Blazor WebAssembly package references remain compatible with the .NET 8 application baseline.
- No unexpected deployment or publish properties have been introduced.

### TMDB Edge Function

Verify the Edge Function source under:

```text
netlify/edge-functions/
```

Confirm that the `tmdb` function:

- Reads `API_KEY` from the Netlify environment.
- Reads `API_URL` from the Netlify environment.
- Does not contain hard-coded credentials.
- Continues to proxy the `/tmdb/*` application route.
- Constructs valid TMDB requests.
- Does not return the API credential to the browser.

### Program.cs

Verify that application service registration remains consistent with the client architecture.

Confirm that:

- Required Blazor services are registered.
- `HttpClient` configuration remains appropriate for the hosted WebAssembly application.
- TMDB-related client services remain registered.
- No secret or server-only configuration has been introduced into client startup.

### TMDBService.cs

Verify that `TMDBService` continues to communicate through the application-owned TMDB proxy route rather than directly exposing authenticated TMDB requests from the browser.

The expected architecture remains:

```text
Blazor Client
    │
    ▼
TMDBService
    │
    ▼
/tmdb/*
    │
    ▼
Netlify Edge Function
    │
    ▼
TMDB API
```

### Repository Verification

Before production promotion, inspect the configuration changes that will be included in the release:

```powershell
git status
git diff origin/main...develop -- netlify.toml
git diff origin/main...develop -- global.json
git diff origin/main...develop -- BlazorCodeChallenge.csproj
git diff origin/main...develop -- netlify/edge-functions
```

Review the results and confirm that all configuration changes are intentional.

Any unexpected configuration change must be investigated before `develop` is promoted to `main`.

### Configuration Verification Result

Configuration is considered release-ready when:

- Deployment-critical files match the intended production baseline.
- No credentials are present in source control.
- The Release build succeeds locally with zero warnings and zero errors.
- The Release publish succeeds locally.
- The Edge Function configuration remains consistent with the TMDB proxy architecture.
- All intentional configuration changes are understood and documented.
- No unexpected configuration changes are included in the release candidate.

## Phase 7 - Deployment Validation

Deployment validation occurs only after a release candidate has passed local validation and has been intentionally approved for production promotion.

Netlify must not be used as the routine validation environment for development work.

### 1. Pre-Deployment Gate

Before promoting `develop` to `main`, confirm:

- The Release build succeeds with zero warnings and zero errors.
- The Release publish succeeds locally.
- Functional testing has succeeded locally where applicable.
- `git diff --check` reports no whitespace errors.
- All intended changes are committed.
- `develop` is synchronized with `origin/develop`.
- Deployment-sensitive configuration has been reviewed.
- No secrets are present in source control.
- The release candidate is intentionally approved for production.

Verify repository state:

```powershell
git status
git branch -vv
git log --oneline --decorate -5
git diff origin/main...develop --stat
```

Do not promote the release if unexpected changes are present.

### 2. Promote the Validated Release

Production deployment is initiated by intentionally promoting the validated `develop` state to `main`.

Before promotion, ensure the working tree is clean.

The release operation should preserve the validated development history and make the approved release state available on `main`.

After promotion, verify that:

- `main` contains the intended release candidate.
- No unrelated changes were introduced.
- The production commit is identifiable in Git history.
- The push to `main` is intentional.

Because `main` is the Netlify production branch, pushing the approved release to `origin/main` initiates the production deployment.

### 3. Deployment Resource Gate

A production push consumes Netlify deployment resources.

Before pushing `main`, confirm that:

- A production deployment is actually required.
- Local validation is complete.
- No additional known changes should be included in the release.
- The release is ready for immediate production smoke testing.

Avoid using repeated production pushes to diagnose issues that can be reproduced locally.

Development fixes should return to `develop`, pass the validation process again, and only then be considered for another production promotion.

### 4. Monitor the Netlify Build

After the intentional production push, monitor the Netlify deployment log.

Verify that the deployment uses:

```text
Production branch: main
```

Confirm the expected build command executes:

```text
dotnet workload install wasm-tools --skip-manifest-update && dotnet publish BlazorCodeChallenge.csproj -c Release
```

### 5. Validate Build Stages

The deployment log should confirm:

- Repository cloned successfully.
- Expected .NET SDK selected.
- Project dependencies restored successfully.
- `wasm-tools` installation completed successfully.
- Blazor project built successfully.
- Release publish completed successfully.
- Publish directory was located.
- TMDB Edge Function was detected and bundled.
- Netlify deployed the contents of the expected `wwwroot` directory.
- Deployment completed without blocking errors.

Expected publish directory:

```text
bin/Release/net8.0/publish/wwwroot
```

Expected Edge Function:

```text
tmdb
```

### 6. WebAssembly Optimization Message

The currently observed Netlify build may report:

```text
Publishing without optimizations. Although it's optional for Blazor, we strongly recommend using `wasm-tools` workload!
```

This message is currently treated as non-blocking because:

- `wasm-tools` installation completes successfully.
- The application publishes successfully.
- Netlify deploys successfully.
- Production smoke testing has succeeded.

Do not classify this message as proof that WebAssembly optimization is functioning.

Further investigation of the Netlify optimization behavior is tracked separately and does not currently block deployment.

### 7. Confirm Deployment Completion

A deployment is considered technically successful when:

- Netlify reports the site as deployed.
- No blocking build errors occurred.
- The expected production commit was deployed.
- The TMDB Edge Function was bundled.
- The production site is reachable.

Technical deployment success does not complete release validation.

The production smoke test in Phase 8 must still pass.

### Deployment Validation Result

Phase 7 is complete when:

- The release was intentionally promoted from `develop` to `main`.
- Only the intended production deployment was triggered.
- The Netlify build completed successfully.
- The expected application artifacts were deployed.
- The Edge Function bundled successfully.
- No blocking deployment errors remain.
- The deployed commit corresponds to the approved release candidate.

The release then proceeds to Phase 8 production functional testing.
	
## Phase 8 - Production Functional Testing

A successful Netlify deployment must be followed by a production smoke test before the release is considered validated.

Testing must be performed against the deployed production site rather than the local development environment.

### 1. Confirm Application Startup

Open the production site in a browser.

Verify that:

- The application loads successfully.
- The Blazor WebAssembly application initializes.
- The primary layout renders correctly.
- Navigation is available.
- No deployment-related error is displayed.

### 2. Home Page

Verify:

- The Home page loads successfully.
- Expected content and branding are displayed.
- Navigation links function correctly.
- Header and footer presentation are correct.

### 3. Weather Dashboard

Open the Weather Dashboard and exercise a representative weather lookup.

Verify:

- The page loads successfully.
- Location lookup succeeds.
- Weather data is returned.
- Forecast information renders correctly.
- No unexpected application error occurs.

### 4. Loan Shark

Open the Loan Shark mortgage calculator.

Verify:

- The page loads successfully.
- Input controls accept values.
- Mortgage calculation succeeds.
- Monthly payment information is displayed.
- Payment schedule information renders correctly.

### 5. FizzBuzz

Open the FizzBuzz application.

Verify:

- The page loads successfully.
- Validation rules operate correctly.
- Valid input produces FizzBuzz results.
- Invalid input displays the expected validation behavior.
- No runtime errors occur.

### 6. Movie Time

Movie Time is the primary end-to-end production integration test because it exercises:

```text
Browser
    │
    ▼
Blazor WebAssembly
    │
    ▼
TMDBService
    │
    ▼
/tmdb/*
    │
    ▼
Netlify Edge Function
    │
    ▼
TMDB API
```

Verify:

- Movie Time loads successfully.
- Live movie data is returned.
- Movie posters and metadata render correctly.
- Movie Details navigation functions correctly.
- Favorites functionality operates correctly.
- An empty favorites collection does not cause an application failure.
- Requests through the TMDB Edge Function succeed.

Successful Movie Time operation provides functional confirmation that the client application, Netlify Edge Function, environment configuration, and TMDB integration are operating together in production.

### 7. Browser Developer Tools

Open the browser developer tools during the smoke test.

Review the Console and Network tabs.

Verify:

- No deployment-related console errors are present.
- No unexpected unhandled application exceptions occur.
- Required application resources load successfully.
- TMDB proxy requests complete successfully.
- The TMDB API key is not exposed in browser requests.
- No sensitive credential appears in client-visible resources.

Third-party browser warnings or unrelated browser-extension messages should be distinguished from application-generated errors.

### 8. Production Release Failure

If any production smoke-test requirement fails:

- Do not treat the release as successfully validated.
- Record the observed failure.
- Determine whether the issue can be reproduced locally.
- Perform corrective development on `develop` or an appropriate `feature/*` branch.
- Repeat the local Release build, publish, and validation gates.
- Do not repeatedly push speculative fixes directly to `main`.

A subsequent production deployment should occur only after the corrective release candidate has passed the normal validation process.

### Functional Testing Result

Phase 8 is complete when:

- Home passes production validation.
- Weather Dashboard passes production validation.
- Loan Shark passes production validation.
- FizzBuzz passes production validation.
- Movie Time passes production validation.
- Browser developer tools reveal no known deployment-related application errors.
- TMDB integration operates successfully through the Edge Function.
- No sensitive credential is exposed to the browser.

After Phase 8 succeeds, the release proceeds to the final acceptance criteria.
	
## Phase 9 - Release Acceptance Criteria

A production release is accepted only after the validated release candidate has successfully completed the local verification, production deployment, and production functional testing procedures defined in this runbook.

### Release Acceptance Requirements

The release is accepted when:

- The approved release candidate was promoted from `develop` to `main`.
- The production deployment was intentionally triggered from `main`.
- The Netlify build completed without blocking errors.
- The expected production commit was deployed.
- The Blazor WebAssembly application loads successfully in production.
- Home passes production validation.
- Weather Dashboard passes production validation.
- Loan Shark passes production validation.
- FizzBuzz passes production validation.
- Movie Time passes end-to-end production validation.
- The TMDB Edge Function operates successfully.
- Browser developer tools reveal no known deployment-related application errors.
- No application credential is exposed to the browser.
- The production smoke test is complete.

### Repository State After Release

After production validation succeeds, verify the repository state:

```powershell
git status
git branch -vv
git log --oneline --decorate -5
```

Confirm that:

- `main` contains the production release that was just validated.
- `develop` contains the development history from which the release was promoted.
- No unintended local changes remain.
- The production commit can be clearly identified in Git history.

After the release has been accepted, active development should return to `develop`:

```powershell
git switch develop
git status
```

Development may then continue on `develop` or an appropriate `feature/*` branch.

### Deployment Resource Consideration

Netlify production deployments are a limited operational resource and should be treated as release events rather than routine development validation.

Until additional deployment capacity is available:

- Development should remain on `develop` or `feature/*`.
- Branch Deploys should remain disabled.
- Deploy Previews should remain disabled.
- Local Release builds and publishes should be used for routine validation.
- Changes should be accumulated into meaningful release candidates.
- Production promotion should occur only when the release candidate is ready for production validation.
- Avoid speculative or diagnostic pushes to `main`.

This process reduces unnecessary deployment consumption while preserving source control, remote backup, and normal development history.

### Release Acceptance Result

A release is considered successfully accepted when:

- All local validation gates have passed.
- The intended release candidate has been promoted to `main`.
- Netlify has successfully deployed the expected production commit.
- Production functional testing has passed.
- TMDB integration has been verified through the Edge Function.
- No known deployment-related application errors remain.
- No sensitive credential is exposed to the browser.
- The repository state has been verified after deployment.

At that point, the deployed `main` branch represents the accepted production state and normal development may resume on `develop`.

## Release Readiness Checklist

The Release Readiness Checklist is the final go/no-go gate before a validated release candidate is promoted to production.

A production deployment should not be initiated until all applicable pre-release items are satisfied.

### Pre-Release Checklist

| Item | Required State |
| --- | --- |
| Development Branch | Release candidate validated on `develop` |
| Working Tree | Clean |
| Release Build | 0 warnings, 0 errors |
| Release Publish | Successful |
| Local Functional Validation | Passed |
| Git Diff Check | No whitespace errors |
| GitHub Synchronization | `develop` synchronized with `origin/develop` |
| Configuration Review | Completed |
| `netlify.toml` | Verified |
| `global.json` | Verified |
| Edge Function | Verified |
| Environment Variables | Verified |
| Secrets Review | No credentials committed or exposed |
| Production Changes | Reviewed and intentional |
| Deployment Resources | Production deployment intentionally approved |

### Netlify Deployment Configuration

Before production promotion, confirm:

- Production branch is `main`.
- Branch Deploys remain disabled.
- Deploy Previews remain disabled.
- `netlify.toml` contains the approved production configuration.
- The publish directory is `bin/Release/net8.0/publish/wwwroot`.
- The TMDB Edge Function mapping remains `/tmdb/*`.
- Required Netlify environment variables are present.
- The production deployment is necessary and ready for immediate smoke testing.

### Production Promotion Gate

Before pushing the approved release to `main`, confirm:

```powershell
git status
git branch -vv
git log --oneline --decorate -5
git diff origin/main...develop --stat
git diff --check
```

The release is GO only when:

- All expected changes are understood.
- No unintended files are included.
- Local validation has completed successfully.
- Deployment-critical configuration has been reviewed.
- No additional known changes need to be added to the release candidate.
- The production deployment has been intentionally approved.

If any requirement is not satisfied, the release is NO-GO and remains on `develop` until the issue is resolved.

### Post-Deployment Checklist

After Netlify reports a successful production deployment:

- Confirm the expected commit was deployed.
- Confirm the production site is reachable.
- Verify the TMDB Edge Function was bundled.
- Complete the Phase 8 production smoke test.
- Review browser Console and Network activity.
- Confirm no application credential is exposed.
- Confirm no known deployment-related errors remain.
- Verify repository state after release.

### Release Readiness Result

A release is considered complete only after both conditions are satisfied:

1. The pre-release checklist resulted in a GO decision.
2. The production deployment and smoke test completed successfully.

Until both conditions are satisfied, the release must not be recorded as an accepted production release.

## Deployment Recovery

Deployment recovery begins when a production deployment fails or when the deployed application fails production validation.

The recovery approach depends on whether the failure occurred during the Netlify build/deployment process or after the application reached production.

### 1. Build or Deployment Failure

If Netlify fails to complete the production deployment:

1. Review the Netlify deployment log.
2. Identify the first relevant build or deployment error.
3. Confirm that the expected `main` commit was used.
4. Verify the .NET SDK and workload configuration.
5. Verify `netlify.toml`.
6. Confirm the expected publish directory was produced.
7. Verify that the TMDB Edge Function bundled successfully.
8. Confirm required Netlify environment variables remain configured.

Do not immediately push another change to `main`.

Determine whether the failure can be reproduced locally first.

### 2. Local Reproduction

Return corrective work to `develop` or an appropriate `feature/*` branch.

After corrective changes have been integrated into `develop`, run the automated local Release validation:

```powershell
.\scripts\validate-release.ps1
```

The automated validation must complete successfully before another production promotion is attempted.

If a validation gate fails or additional diagnosis is required, use the individual Phase 1 validation procedures to isolate and investigate the failure.

The corrected release candidate must pass all normal release gates before another production promotion is attempted.

### 3. Production Functional Failure

If Netlify deploys successfully but the Phase 8 production smoke test fails:

- Record the failing feature and observed behavior.
- Review browser Console and Network activity.
- Determine whether the failure affects the entire application or an isolated feature.
- Verify the TMDB Edge Function and environment configuration when Movie Time is affected.
- Attempt to reproduce the problem locally.
- Perform corrective development on `develop` or `feature/*`.
- Repeat local validation before considering another production deployment.

Do not use repeated production deployments as the primary debugging process.

### 4. Rollback Decision

If the newly deployed production release introduces a significant regression, restoring the previous known-good production state may be preferable to waiting for a corrective release.

Before rollback:

- Identify the last known-good production commit.
- Confirm that the commit represents a previously validated production state.
- Determine whether rollback is safer than leaving the current release active.
- Preserve the failed release information for later diagnosis.

A rollback is an operational recovery action and should not replace correcting the underlying issue on `develop`.

### 5. Corrective Release

After the problem has been corrected:

1. Validate the fix locally.
2. Commit the corrective work to the appropriate development branch.
3. Integrate the correction into `develop` if necessary.
4. Run the automated local Release validation from `develop`.
5. Review the release diff.
6. Promote the corrected release candidate to `main` only after it passes the normal release gates.
7. Perform the complete production smoke test again.

### 6. Deployment Resource Protection

Because production deployments consume limited Netlify resources:

- Investigate locally whenever possible.
- Avoid speculative pushes to `main`.
- Combine related corrections into a validated release candidate when practical.
- Do not trigger a new production deployment merely to gather information that can be obtained locally.
- Reserve production deployment for validated releases, necessary corrective releases, and intentional rollback operations.

### Recovery Result

Recovery is complete when one of the following states has been achieved:

- The previous known-good production state has been restored, or
- A corrected release has successfully passed deployment and production validation.

Any underlying defect discovered during recovery should remain tracked until it has been corrected in the active development branch.

## Sprint 1 Summary

Sprint 1 successfully established the initial production deployment pipeline for BlazorExpo.

Major accomplishments included:

- GitHub integration
- Automated Netlify deployments
- TMDB Edge Function implementation
- Secure API key management
- Successful production deployment
- Functional validation of all application modules
- Production-ready deployment documentation
- Operational runbook and engineering documentation

## Sprint 2 Status

Sprint 2 builds upon the RC1 production baseline by improving build quality, deployment efficiency, and release discipline.

Completed Sprint 2 improvements include:

- Resolved the remaining nullable reference warnings.
- Removed the unused Weather Dashboard exception variable.
- Achieved a Release build with zero warnings and zero errors.
- Verified successful local Release publishing.
- Evaluated and installed the Blazor WebAssembly `wasm-tools` workload locally.
- Added `wasm-tools` installation to the Netlify production build process.
- Verified successful Netlify deployment with the workload installation step.
- Established `develop` as the active integration branch.
- Reserved `main` for intentional production releases.
- Disabled Netlify Branch Deploys.
- Disabled Netlify Deploy Previews.
- Established local validation as the primary development verification process.
- Added `scripts/validate-release.ps1` to automate the standard local Release validation workflow.
- Removed the obsolete `netlify.build.sh` script after confirming that `netlify.toml` is the authoritative Netlify production build configuration.
- Added in-memory caching to `MovieFavoritesService` to reduce repeated `localStorage` access while preserving browser persistence.
- Introduced deployment-resource-aware release procedures.
- Expanded the Operations Runbook to document the Sprint 2 release workflow.

### Current Sprint 2 State

The application currently has a verified local Release build and publish baseline.

Movie Time favorites now use an in-memory cache for the lifetime of `MovieFavoritesService`. Favorites are loaded from browser `localStorage` when the cache is first initialized, subsequent favorite lookups use the cached collection, and changes are persisted back to `localStorage`.

The Movie Favorites caching change was validated through a clean Release build, successful Release publish, functional smoke testing, and browser developer-tools verification.

The Netlify `wasm-tools` installation succeeds; however, the observed Netlify publish process continues to report that publishing occurs without WebAssembly optimizations. This behavior is treated as non-blocking and remains available for future investigation.

Production deployment capacity is currently being conserved. Development work should continue on `develop` or `feature/*` branches and should not be promoted to `main` until an intentional production release is warranted.

## Known Issues and Future Improvements

Sprint 2 resolved the cleanup items originally deferred from Sprint 1, including nullable reference warnings, the unused Weather Dashboard exception variable, and initial evaluation of the Blazor WebAssembly `wasm-tools` workload.

The following items remain available for future investigation or enhancement.

### Known Issues

#### Netlify WebAssembly Optimization

The Netlify production build successfully installs the `wasm-tools` workload before publishing.

However, the observed Netlify publish process continues to report:

```text
Publishing without optimizations. Although it's optional for Blazor, we strongly recommend using `wasm-tools` workload!
```

The application builds, deploys, and operates successfully despite this message.

This issue is currently considered non-blocking.

Future investigation may determine why the Netlify publish environment does not recognize the installed workload for WebAssembly optimization.

### Future Improvements

Potential future improvements include:

- Investigate the remaining Netlify WebAssembly optimization behavior.
- Evaluate additional automated validation and testing where it provides meaningful release confidence.
- Evaluate additional application performance optimizations where measurable benefits exist.
- Expand operational documentation as the release process evolves.

### Operational Priority

Until additional Netlify deployment capacity is available, development should continue using the established Sprint 2 workflow:

```text
feature/*
    │
    ▼
develop
    │
    ▼
Local Validation
    │
    ▼
Validated Release Candidate
    │
    ▼
main
    │
    ▼
Netlify Production Deployment
```

Production deployments should remain intentional release events rather than routine development validation.

### Current Engineering Baseline

At the current Sprint 2 baseline:

- Release builds complete with zero warnings and zero errors.
- Local Release publishing succeeds.
- `develop` is the active integration branch.
- `main` is reserved for production-ready releases.
- Netlify Branch Deploys are disabled.
- Netlify Deploy Previews are disabled.
- `netlify.toml` is the authoritative Netlify production build configuration; the obsolete `netlify.build.sh` script has been removed.
- TMDB credentials remain protected by the Netlify Edge Function.
- Movie Time favorites use an in-memory cache backed by browser `localStorage` persistence.
- `scripts/validate-release.ps1` provides the standard automated local Release validation workflow.
- Local validation is the primary development verification process.
- Production deployment resources are intentionally conserved.
- The remaining WebAssembly optimization message is documented as non-blocking.

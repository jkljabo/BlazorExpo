# Engineering Decision Log

**Project:** BlazorExpo

**Document Version:** 1.0

**Release:** RC1 (Sprint 1)

**Last Updated:** July 2026

**Owner:** Jason Little

---

## Purpose

This document records significant engineering and architectural decisions made during the development of BlazorExpo.

Each decision documents:

- The problem being solved
- The alternatives considered
- The decision made
- The reasoning behind the decision
- The resulting impact

This document serves as a historical record for future development.

## EDR-001

### Status
Accepted

### Title

GitHub as the Source of Truth

### Date

July 2026

### Problem

A repeatable, traceable deployment process was needed to eliminate manual publishing and ensure every production deployment could be tied to a specific version of the source code.

### Alternatives Considered

- Manual ZIP deployment
- Manual publish from Visual Studio
- GitHub-integrated continuous deployment

### Decision

GitHub was selected as the authoritative source for all production deployments.

### Rationale

Using GitHub as the deployment source provides version control, traceability, automated deployments, and reproducible builds. Every deployment originates from a committed and reviewable codebase rather than an unpublished local copy.

### Impact

- Every deployment is associated with a specific commit.
- Netlify automatically deploys changes promoted and pushed to the `main` branch.
- Development branches can be pushed to GitHub without triggering production deployments.
- Rollbacks can be performed by redeploying a previous commit.
- Local builds are no longer considered deployment artifacts.

## EDR-002

### Status
Accepted

### Title

Netlify as the Hosting Platform

### Date

July 2026

### Problem

A hosting platform was required for a Blazor WebAssembly application that could support automated deployments, secure environment variables, and server-side request proxying without introducing a dedicated backend.

### Alternatives Considered

- Azure Static Web Apps
- IIS
- GitHub Pages
- Netlify

### Decision

Netlify was selected as the hosting platform.

### Rationale

Netlify provides seamless GitHub integration, automated deployments, environment variable management, and Edge Functions, allowing the application to remain a static Blazor WebAssembly site while securely accessing external APIs.

### Impact

- Changes pushed to the production `main` branch automatically trigger Netlify deployments.
- Environment variables remain server-side.
- No dedicated application server is required.
- Deployment complexity is significantly reduced.

## EDR-003

### Status
Accepted

### Title

Edge Function Proxy for TMDB

### Date

July 2026

### Problem

The TMDB API requires an API key that must not be exposed to browser clients.

### Alternatives Considered

- Direct browser requests
- Custom ASP.NET Core API
- Azure Functions
- Netlify Edge Functions

### Decision

A Netlify Edge Function was implemented as a secure proxy between the Blazor client and the TMDB API.

### Rationale

The Edge Function keeps API credentials within the Netlify environment while allowing the application to remain a static Blazor WebAssembly application. This avoids the complexity of maintaining a separate backend while protecting sensitive credentials.

### Impact

- TMDB API keys remain confidential.
- Browser clients communicate only with the Edge Function.
- The application maintains a serverless architecture.
- Security is improved without increasing infrastructure complexity.

## EDR-004

### Status
Accepted

### Title

SDK Version Strategy

### Date

July 2026

### Problem

Local SDK newer than Netlify SDK.

### Alternatives Considered

- Pin latest SDK
- Remove global.json
- Align with Netlify

### Decision

Align global.json with the deployment environment while allowing roll-forward.

### Rationale

Local development used a newer .NET SDK than the version available in Netlify's build environment. Rather than removing SDK version management or forcing developers to downgrade locally, `global.json` was aligned with the supported deployment SDK while enabling roll-forward for compatible local SDK versions.

### Impact

- Local development remains flexible.
- CI builds remain predictable.
- Netlify deployments use a supported SDK.
- SDK version conflicts are minimized.

## EDR-005

### Status
Accepted

### Title

Centralized Routing Configuration

### Date

July 2026

### Problem

Routing existed in both _redirects and netlify.toml.

### Alternatives Considered

- Maintain both `_redirects` and `netlify.toml`
- Use `_redirects` exclusively
- Use `netlify.toml` exclusively

### Decision

`netlify.toml` became the single source of truth for all Netlify routing configuration.

### Rationale

Maintaining routing rules in two locations introduced unnecessary duplication and increased the risk of inconsistent behavior. Consolidating configuration into `netlify.toml` simplified maintenance and aligned with Netlify's recommended configuration approach.

### Impact

- One routing configuration to maintain.
- Elimination of duplicate redirect rules.
- Simplified deployment configuration.

## EDR-006

### Status
Accepted

### Title

Client Architecture

### Date

July 2026

### Problem

Need to consume TMDB securely while remaining a Blazor WebAssembly application.

### Alternatives Considered

- Direct browser communication with TMDB
- Dedicated ASP.NET Core backend
- Repository pattern with backend API
- Client-service-proxy architecture using Netlify Edge Functions

### Decision

The application uses a client-service-proxy architecture.

```text
Browser
    │
    ▼
TMDBService
    │
    ▼
Netlify Edge Function
    │
    ▼
TMDB API
```

### Rationale

This architecture separates presentation, application logic, and external API access while protecting sensitive credentials. The Blazor client communicates only with application services, which in turn communicate with the Edge Function responsible for external API requests.

### Impact

- Secure API communication.
- Clear separation of responsibilities.
- Static hosting maintained.
- Architecture remains extensible for additional APIs.

## EDR-007

### Status
Accepted

### Title

Operational Documentation

### Date

July 2026

### Problem

Project knowledge often lives only in source code or commit history.

### Alternatives Considered

- Source code only
- README-only documentation
- Operational documentation alongside source code

### Decision

OperationsRunbook.md and EngineeringDecisionLog.md were established as version-controlled project documentation.

### Rationale

Deployment procedures and architectural decisions are as important as the source code itself. Versioning this documentation alongside the application ensures operational knowledge evolves with the project and is not lost over time.

### Impact

- Faster onboarding for future contributors.
- Improved troubleshooting.
- Historical record of engineering decisions.
- Repeatable release process.

## EDR-008

### Status
Accepted

### Title

Development and Release Branch Strategy

### Date

July 2026

### Problem

Active development was being performed directly against the `main` branch, which also serves as the Netlify production deployment branch.

This coupled normal development activity with production deployment and caused routine pushes to consume Netlify build and deployment resources. A controlled separation between active development and production releases was needed.

### Alternatives Considered

- Continue development directly on `main`
- Use feature branches that merge directly into `main`
- Introduce a `develop` integration branch with optional feature branches and reserve `main` for production releases

### Decision

BlazorExpo uses `develop` as the integration branch for active development and Sprint work.

The `main` branch represents production-ready code and remains the only branch configured for Netlify production deployment.

Feature branches may be created for isolated development tasks and integrated into `develop` after local validation.

The release flow is:

```text
feature/* → develop → validation → main → Netlify → production smoke test
```

### Rationale

Separating development from the production branch establishes a clear release boundary while allowing work to remain version-controlled and backed up remotely.

The strategy supports incremental development, local verification, controlled production promotion, and more efficient use of limited hosting and deployment resources.

### Impact

- Active Sprint development occurs on `develop` or `feature/*` branches.
- Development work can be pushed to GitHub without triggering Netlify production deployments.
- `main` remains synchronized with the production release state.
- Promotion to `main` becomes an intentional release operation.
- Netlify Branch Deploys are disabled.
- Netlify Deploy Previews are disabled.
- Production deployments are reserved for validated release candidates.
- Deployment resources can be managed more efficiently.

## Conclusion

The decisions recorded in this document establish and maintain the engineering baseline for BlazorExpo.

EDR-001 through EDR-007 preserve the architectural and operational decisions established for Release Candidate 1 during Sprint 1. EDR-008 extends that baseline with the development and production release strategy introduced during Sprint 2.

Future Engineering Decision Records should build upon these decisions while preserving the historical context that led to the current architecture and operational model.
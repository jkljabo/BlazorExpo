# BlazorExpo

BlazorExpo is a .NET 8 Blazor WebAssembly portfolio application showcasing modern application development, cloud integration, secure API architecture, and automated deployment practices.

The application brings together several interactive projects within a single Blazor WebAssembly application and serves as both a technical demonstration and an evolving software engineering portfolio.

## Live Application

BlazorExpo is publicly deployed on Netlify:

[View BlazorExpo](https://jasonlittle-dev.netlify.app)

## Featured Applications

### Weather Dashboard

A weather application that retrieves location and forecast data from external APIs and presents current weather information through an interactive Blazor interface.

### Loan Shark

A mortgage calculator that calculates monthly payments and generates an amortization/payment schedule based on user-provided loan information.

### FizzBuzz

An interactive implementation of the classic FizzBuzz programming exercise demonstrating application logic, validation, and Blazor component interaction.

### Movie Time

A movie discovery application powered by The Movie Database (TMDB).

Features include:

- Browse currently playing movies
- Search for movies
- View movie details
- View movie trailers
- Manage favorite movies using browser local storage
- Secure TMDB API access through a Netlify Edge Function

## Technology Stack

### Application

- C#
- .NET 8
- Blazor WebAssembly
- Razor Components
- JavaScript Interop
- HTML
- CSS

### APIs and Integration

- REST APIs
- TMDB API
- National Weather Service API
- Geocoding services
- Netlify Edge Functions

### Cloud and DevOps

- GitHub
- Netlify
- GitHub-integrated continuous deployment
- Environment-based secret management
- Serverless Edge Functions

## Architecture

BlazorExpo is deployed as a static Blazor WebAssembly application.

External services that do not require protected credentials can be accessed through application services, while APIs requiring secrets are accessed through secure server-side proxies.

The Movie Time integration uses the following architecture:

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

## Deployment

BlazorExpo uses a GitHub-driven continuous deployment workflow.

```text
Local Development
        │
        ▼
       Git
        │
        ▼
      GitHub
        │
        ▼
      Netlify
        │
        ▼
  Deployed Site
```

Changes committed and pushed to the `main` branch automatically trigger a Netlify build and deployment.

The release workflow follows:

```text
Build → Verify → Commit → Push → Deploy → Validate
```

## Engineering Practices

BlazorExpo demonstrates software engineering and operational practices including:

- Source-controlled deployments
- Automated CI/CD
- Environment-based secret management
- Serverless API proxying
- Separation of concerns
- Dependency injection
- Service-oriented application design
- External REST API integration
- Deployment validation
- Operational documentation
- Engineering decision documentation

## Project Documentation

BlazorExpo maintains version-controlled engineering and operational documentation alongside the application source code.

### Operations Runbook

[`OperationsRunbook.md`](docs/OperationsRunbook.md)

Documents the verified procedures for:

- Local builds and publishing
- GitHub synchronization
- Netlify configuration
- Deployment validation
- Functional testing
- Deployment recovery

### Engineering Decision Log

[`EngineeringDecisionLog.md`](docs/EngineeringDecisionLog.md)

Records significant architectural and engineering decisions, including:

- GitHub as the deployment source of truth
- Netlify as the hosting platform
- Secure TMDB Edge Function proxying
- .NET SDK version strategy
- Centralized routing configuration
- Client-service-proxy architecture

### Sprint 1 Retrospective

[`Sprint1-Retrospective.md`](docs/Sprint1-Retrospective.md)

Documents:

- Sprint objectives and outcomes
- Major accomplishments
- Challenges encountered
- Lessons learned
- Areas for improvement
- Items deferred to Sprint 2

## Release Status

**Current Release:** RC1 (Sprint 1)

Sprint 1 established the initial deployment and operational baseline for BlazorExpo.

RC1 includes:

- Automated GitHub-to-Netlify deployments
- Secure TMDB API integration through a Netlify Edge Function
- Environment-based secret management
- Verified application functionality
- Centralized Netlify deployment and routing configuration
- Operational and engineering documentation

BlazorExpo RC1 provides the stable foundation for continued development in Sprint 2.

## Roadmap

Planned areas of focus for Sprint 2 and future development include:

- Address outstanding compiler warnings, including nullable reference warnings and unused code
- Evaluate the Blazor WebAssembly `wasm-tools` workload for publish optimization
- Improve Movie Favorites state management and caching
- Continue application performance improvements
- Enhance user experience and visual presentation
- Expand portfolio demonstrations and application features

## About the Developer

BlazorExpo was developed by **Jason Little**, a Senior .NET / Azure Engineer specializing in enterprise application development, cloud modernization, and software architecture.

Core areas of expertise include:

- C# and .NET
- ASP.NET Core and Blazor
- Microsoft Azure
- REST API design and integration
- SQL Server
- Cloud modernization
- Enterprise application architecture
- CI/CD and DevOps
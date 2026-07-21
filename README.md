# BlazorExpo

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet) ![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor) ![Netlify](https://img.shields.io/badge/Hosted%20on-Netlify-00C7B7?logo=netlify) ![GitHub Tag](https://img.shields.io/github/v/tag/jkljabo/BlazorExpo?label=Release)

BlazorExpo is an enterprise-style Blazor WebAssembly application demonstrating modern .NET development practices, cloud-native architecture, secure API integration, and production deployment workflows. It serves as both a functional application suite and the flagship project of the Engineering Showcase Suite.

The application brings together several interactive projects within a single Blazor WebAssembly application and serves as both a technical demonstration and an evolving software engineering portfolio.

## Live Application

BlazorExpo is publicly deployed on Netlify:

<img width="1470" height="963" alt="BlazorExpo application home page" src="https://github.com/user-attachments/assets/e7b6a293-10eb-4c4f-be05-7b78d8fe63a8" />

**[Launch BlazorExpo](https://jasonlittle-dev.netlify.app)**

## Featured Applications

### Weather Dashboard

#### Purpose

Provides location-based weather forecasts using live government and geolocation APIs.

#### Technical Highlights

- REST API integration
- Dependency injection
- Asynchronous programming
- Geolocation and geocoding
- JSON serialization and parsing
- Service-oriented application design

<img width="1469" height="964" alt="Weather Dashboard displaying location-based forecast information" src="https://github.com/user-attachments/assets/23ff77ca-c87f-4896-8dbe-d1d0817df703" />

### Loan Shark

#### Purpose

Provides an interactive mortgage calculator that calculates monthly payments and generates a complete loan amortization schedule.

#### Technical Highlights

- Financial calculation algorithms
- C# business logic
- Blazor component interaction
- User input validation
- Data binding
- Dynamic UI rendering
- Amortization schedule generation

<img width="1469" height="964" alt="Loan Shark mortgage calculator and payment schedule" src="https://github.com/user-attachments/assets/6277179c-aa85-491b-8768-1d367d5d92af" />

### FizzBuzz

#### Purpose

Provides an interactive implementation of the classic FizzBuzz programming challenge while demonstrating validation, component interaction, and application logic within Blazor.

#### Technical Highlights

- C# conditional logic
- Blazor component lifecycle
- Form handling
- Custom validation
- Data binding
- User input validation
- Dynamic UI rendering

<img width="1495" height="992" alt="FizzBuzz interactive application" src="https://github.com/user-attachments/assets/9d6ddc35-579f-4077-bef7-6a4e7b2816f9" />

### Movie Time

#### Purpose

Provides a movie discovery experience powered by The Movie Database (TMDB), allowing users to browse, search, explore, and save favorite movies.

#### Technical Highlights

- REST API integration
- Asynchronous service calls
- Dependency injection
- Browser local storage
- JavaScript interoperability
- Secure API credential management
- Netlify Edge Functions
- Serverless API proxy architecture
- Environment-based secret management

<img width="1496" height="994" alt="Movie Time movie discovery dashboard" src="https://github.com/user-attachments/assets/1c6f4a72-b269-48cf-b711-e9ca0c34b306" />

#### Features

- Browse currently playing movies
- Search for movies
- View movie details
- View movie trailers
- Manage favorite movies using browser local storage
- Secure TMDB API access through a Netlify Edge Function

<img width="1495" height="722" alt="Movie Time currently playing movie results" src="https://github.com/user-attachments/assets/912097d8-99d6-4c16-8362-0cf7e4b0a032" />

<img width="1492" height="994" alt="Movie Time favorite movies interface" src="https://github.com/user-attachments/assets/5b11f24a-d8eb-4f87-ae21-0985e733edcc" />

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

## Local Development

### Prerequisites

To build and run BlazorExpo locally, install:

- .NET 8 SDK
- Git
- Visual Studio 2022, Visual Studio Code, or another .NET-compatible development environment

### Clone the Repository

```bash
git clone https://github.com/jkljabo/BlazorExpo.git
cd BlazorExpo
```

### Configure Local Development Settings

BlazorExpo uses a local development configuration file for credentials that should not be committed to source control.

An example configuration file is provided at:

```text
wwwroot/appsettings.Development.example.json
```

Create a local copy named:

```text
wwwroot/appsettings.Development.json
```

and provide your TMDB API access token in the configuration.

The local `appsettings.Development.json` file is excluded from Git through `.gitignore` to prevent credentials from being committed to the repository.

> Never commit API keys, access tokens, passwords, or other secrets to source control.

### Restore and Build

Restore dependencies and build the application:

```bash
dotnet restore BlazorCodeChallenge.csproj
dotnet build BlazorCodeChallenge.csproj
```

A successful build should complete with no build errors.

### Run the Application

Start the application locally with:

```bash
dotnet run --project BlazorCodeChallenge.csproj
```

The development server will display the local application URL in the console.

### Local vs. Deployed API Configuration

During local development, Movie Time can use the TMDB access token provided through the local development configuration.

In the deployed Netlify environment, protected TMDB credentials are not exposed to the Blazor WebAssembly client. Requests requiring authentication are routed through a Netlify Edge Function, where the API credential is supplied through environment-based secret management.

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

**Current Release:** `v1.0.0-rc1` (Sprint 1 Release Candidate)

Sprint 1 established the initial deployment and operational baseline for BlazorExpo.

`v1.0.0-rc1` includes:

- Automated GitHub-to-Netlify deployments
- Secure TMDB API integration through a Netlify Edge Function
- Environment-based secret management
- Verified application functionality
- Centralized Netlify deployment and routing configuration
- Operational and engineering documentation

BlazorExpo `v1.0.0-rc1` provides the stable foundation for continued development in Sprint 2.

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
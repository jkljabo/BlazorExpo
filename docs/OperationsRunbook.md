# BlazorExpo Operations Runbook

**Project:** BlazorExpo

**Document Version:** 1.0

**Release:** RC1 (Sprint 1)

**Last Updated:** July 2026

**Owner:** Jason Little

## Purpose

This runbook documents the verified deployment, validation, and operational procedures for BlazorExpo. It serves as the authoritative guide for building, deploying, validating, and troubleshooting the application using GitHub and Netlify.

## Scope

This document reflects the verified Release Candidate 1 (RC1) deployment process established during Sprint 1 and serves as the operational baseline for future releases.

## Phase 1 - Local Verification (Visual Studio)

1. Verify the solution builds:
	dotnet clean BlazorCodeChallenge.csproj
	dotnet restore BlazorCodeChallenge.csproj
	dotnet build BlazorCodeChallenge.csproj
	
	Results:
		Errors: 0
		Warnings: 6 (address during cleanup)
		
2. Verify publish succeeds:
	dotnet publish BlazorCodeChallenge.csproj -c Release
	
	Results:
		bin/Release/net8.0/publish/wwwroot
		
3. Verify Git status:
	git status
						
	Results: 
        On branch main 
        Your branch is up to date with 'origin/main'.
        nothing to commit, working tree clean
		
4. Push latest commit:
	git push origin main
	
	Results: Everything up-to-date

## Phase 2 - Repository Verification (GitHub)

Before touching Netlify, verify:

✓ latest commit visible

✓ netlify.toml matches baseline

✓ global.json matches baseline

✓ tmdb.js matches baseline

✓ Program.cs verified

✓ BlazorCodeChallenge.csproj verified

Everything should match our local repository.

Results: Confirmed
		
## Phase 2.5 – Netlify Compliance

### netlify.toml

Status: Verified

The project uses `netlify.toml` as the single source of truth for Netlify configuration.

Verified settings include:

- Build command
- Publish directory
- Edge Function declaration
- SPA redirect configuration

The legacy `wwwroot/_redirects` file has been removed to eliminate duplicate routing configuration.

---

### global.json

Status: Verified

The project targets the .NET 8 SDK.

`global.json` has been aligned with the Netlify build environment while allowing roll-forward for newer local SDKs.

This configuration has been verified through successful deployment.

---

### tmdb.js

Status: Verified

The TMDB Edge Function has been verified to:

- Read API credentials from Netlify environment variables.
- Proxy requests to TMDB.
- Bundle successfully during deployment.
- Serve Movie Time requests correctly.
		
## Phase 3 – Netlify Project

Verified Configuration

Provider: GitHub

Repository: BlazorExpo

Deployment Branch: main

Status: Verified during RC1 deployment.
		

## Phase 4 - Build Settings

1. Team:
	Jabo's BlazorExpo
	
2. Project name:
	jasonlittle-dev

3. Branch to deploy:
	main
	
4. Base Directory:
	Blank.
	
5. Build Command:
	dotnet publish BlazorCodeChallenge.csproj -c Release
	
6. Publish Directory:
	bin/Release/net8.0/publish/wwwroot
	
7. Functions Directory:
	Blank (unless we later add serverless functions)
	
8. Edge Functions Directory:
	Default: netlify/edge-functions
	Note: Not a field in Netlify
	No custom path unless we intentionally change it.
	
Results:

Verified during RC1 deployment.

Final deployment settings:

• Team: Jabo's BlazorExpo
• Site Name: jasonlittle-dev
• Branch: main
• Base Directory: (blank)
• Build Command:
  dotnet publish BlazorCodeChallenge.csproj -c Release
• Publish Directory:
  bin/Release/net8.0/publish/wwwroot
• Functions Directory:
  (blank)
• Edge Functions Directory:
  netlify/edge-functions (default)

These settings have been validated through successful GitHub-triggered deployments.
	
## Phase 5 - Environment Variables

1. API_KEY
	Stored only in Netlify. Never committed to GitHub.
	
	Results:
		This becomes part of our security documentation.

2. API_URL
	https://api.themoviedb.org/3
	
	Verify:
		Correct spelling
		No extra spaces
		No trailing slash (unless our code expects it)
		
Results: Confirmed

### Security Notes

- TMDB API credentials are stored exclusively as Netlify environment variables.
- Secrets are never committed to source control.
- Client-side requests are proxied through a Netlify Edge Function.
- The browser never receives the TMDB API key.
	
## Phase 6 - Configuration Files

Before each deployment, verify the following files are committed and synchronized with GitHub:

1. netlify.toml
	Verify this matches the current deployment baseline.

2. global.json
	Verify it matches the current SDK strategy.

3. tmdb.js
	Verify:
	Correct export style
	Correct route configuration
	Current Edge Function API
	
4. Program.cs
	Verified
	
5. BlazorCodeChallenge.csproj
	Verified
	
6. TMDBService.cs
	Verified
	
	Results: Confirmed
	
## Phase 7 - Deployment Validation

1. Watch the deployment log.

2. Checklist:
	Repository cloned
	.NET restored
	Project published
	Publish directory found
	Edge Function detected
	Deployment succeeds

	Note: This is where we'll use our Netlify connector to inspect the results if anything is unexpected.
	
Results:

Verified.

Successful deployment includes:

✓ Repository cloned
✓ .NET SDK restored
✓ Project published
✓ Edge Function bundled
✓ Site deployed
✓ Deployment completed without blocking errors
	
## Phase 8 - Functional Testing

Test in this order:
	1. Home page
	2. Weather Dashboard
	3. Loan Shark
	4. FizzBuzz
	5. Movie Time
	6. Browser Console

	Note: Movie Time is last because it's our primary deployment validation.
	
Results:

Verified during RC1 deployment.

The following application features were validated:

✓ Home
✓ Weather Dashboard
✓ Loan Shark
✓ FizzBuzz
✓ Movie Time
✓ Browser Console

Movie Time required one post-release fix to correctly handle an empty localStorage favorites list during first-time application use.
	
## Phase 9 - Acceptance Criteria:

1. Sprint 1 is complete when:
	GitHub push triggers Netlify automatically
	Build succeeds
	Movie Time displays live movies
	Browser console shows no deployment-related errors
	Site is ready to share
	
Results:

Verified.

Sprint 1 acceptance criteria successfully achieved.

• GitHub automatically triggers Netlify deployments.
• Build completes successfully.
• Edge Function bundles correctly.
• Movie Time retrieves live TMDB data.
• Browser console contains no known deployment-related errors.
• Site is publicly accessible.
	
## Release Readiness Checklist

Deployment Baseline Checklist:

We'll treat this as our final go/no-go checklist before a production release.

Item							Status
Local Build						✅
Local Publish					✅
GitHub Connected				✅
Netlify Connected				✅
Deployment Plan					✅
Environment Variables			✅
Documentation					✅ (after Sprint 1 closeout)
Edge Function Review			✅
Working Tree Clean				✅ (prior to release)

Sprint 1 Status:
Release Candidate 1 Successfully Completed

## Deployment Recovery

If a deployment fails:

1. Review the Netlify deployment log.
2. Verify global.json matches the supported SDK.
3. Confirm Environment Variables are present.
4. Verify the Edge Function bundled successfully.
5. Confirm GitHub contains the expected commit.
6. Redeploy the latest successful commit if necessary.

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

## Known Issues and Future Improvements

The following items are known and have been deferred to Sprint 2:

### Deferred Issues

- Remaining nullable reference warnings (CS8618, CS8602).
- WeatherDashboard contains an unused exception variable.

### Planned Improvements

- Evaluate the Blazor WebAssembly wasm-tools workload.
- Cache Movie Favorites in memory.
- Resolve remaining nullable reference warnings.
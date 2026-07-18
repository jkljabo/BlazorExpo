# Sprint 1 Retrospective

**Project:** BlazorExpo

**Sprint:** Sprint 1

**Release:** RC1

**Date:** July 2026

**Owner:** Jason Little

---

## Sprint Objective

Establish a reliable, repeatable, and secure production deployment process for BlazorExpo using GitHub and Netlify.

The sprint focused on moving the application from an existing manually deployed state to a source-controlled deployment pipeline where GitHub serves as the authoritative source and changes to the `main` branch can be automatically built and deployed to Netlify.

The sprint also included securing the Movie Time TMDB integration, validating the production application, and establishing the operational documentation required to support future releases.

## Sprint Outcome

Sprint 1 successfully achieved its primary objective.

BlazorExpo is now deployed through an automated GitHub-to-Netlify deployment pipeline. The application builds successfully in the Netlify environment, the TMDB integration operates through a secure Netlify Edge Function, and the deployed application has passed functional validation.

The production deployment process is documented and repeatable, providing a stable operational baseline for future development.

## Major Accomplishments

- Established GitHub as the authoritative deployment source.
- Created a clean Netlify project connected directly to the GitHub repository.
- Established automatic deployments from the `main` branch.
- Configured the .NET 8 build and publish process for Netlify.
- Resolved differences between local and Netlify .NET SDK environments.
- Implemented and successfully deployed a Netlify Edge Function for TMDB API requests.
- Protected TMDB credentials using Netlify environment variables.
- Consolidated Netlify routing configuration into `netlify.toml`.
- Removed the redundant `wwwroot/_redirects` configuration.
- Successfully deployed and validated all primary BlazorExpo application modules.
- Identified and resolved the Movie Favorites first-use `localStorage` issue.
- Created and finalized the project Operations Runbook.
- Created the Engineering Decision Log for RC1.
- Established a repeatable deployment and validation process for future releases.

## What Went Well

### Incremental Verification

Breaking the deployment process into distinct verification phases helped isolate problems and prevented multiple configuration changes from being introduced simultaneously.

The workflow evolved into:

Build → Verify → Commit → Push → Deploy → Validate

This approach proved particularly useful when troubleshooting Netlify configuration and deployment failures.

### GitHub as the Deployment Source

Moving away from manual and ZIP-based deployments created a traceable and reproducible deployment process.

Each production deployment can now be associated with a specific Git commit, improving release traceability and simplifying future rollback procedures.

### Deployment Troubleshooting

The initial Netlify deployment failure provided useful information about the difference between the local .NET SDK environment and the Netlify build environment.

The issue was identified through deployment logs and resolved by aligning the SDK strategy with Netlify while preserving compatibility with newer local SDK versions.

### Secure TMDB Integration

The TMDB integration was successfully moved behind a Netlify Edge Function.

This allowed the application to remain a static Blazor WebAssembly application while preventing the TMDB API credentials from being exposed to browser clients.

### Production Validation

Testing the deployed application rather than relying solely on local testing identified an issue with the Movie Favorites implementation when `localStorage` did not yet contain favorites data.

The issue was corrected, deployed, and successfully validated.

## What Could Be Improved

### Reduce Analysis Before Testing

Some deployment questions required investigation, but several could ultimately only be answered through controlled deployment testing.

Future work should continue to research important technical decisions while recognizing when a small, reversible experiment can provide a faster and more definitive answer.

### Establish the Deployment Baseline Earlier

The project initially contained overlapping or evolving deployment configuration, including routing definitions in both `_redirects` and `netlify.toml`.

Future deployment work should establish the authoritative configuration source earlier in the process.

### Environment Compatibility Verification

The first deployment attempt failed because the SDK requested by `global.json` was not available in the Netlify build environment.

Future deployment preparation should explicitly verify runtime and SDK compatibility with the target hosting environment before the first production build.

### Production-Like Testing

The Movie Favorites issue demonstrated that application state can differ between development environments and first-time production users.

Future validation should include:

- Clean browser cache
- Empty localStorage
- First-time application use
- Browser console inspection

## Challenges Encountered

### .NET SDK Version Mismatch

The initial Netlify build failed because `global.json` requested an SDK version that was not installed in the Netlify build environment.

The SDK strategy was revised to align with the deployment environment while allowing compatible local SDK versions to roll forward.

### Edge Function Configuration

The correct Netlify Edge Function file structure, export style, routing declaration, and configuration ownership required verification.

The final configuration successfully bundles and deploys the `tmdb` Edge Function.

### Duplicate Routing Configuration

Routing rules existed in both `wwwroot/_redirects` and `netlify.toml`.

The duplicate configuration produced unnecessary deployment warnings and created multiple potential sources of truth.

Routing was consolidated into `netlify.toml`.

### Movie Favorites Initialization

The deployed application exposed a first-use scenario where `localStorage.getItem()` returned no existing favorites data.

The Movie Favorites service was updated to gracefully handle an empty favorites store.

## Lessons Learned

1. Deployment logs should be treated as primary diagnostic evidence.
2. Local build success does not guarantee cloud build compatibility.
3. Deployment configuration should have a clearly defined single source of truth.
4. Secrets used by Blazor WebAssembly applications must never be assumed to be secure simply because they originate from configuration.
5. Serverless and Edge Function architectures can provide secure backend capabilities without requiring a dedicated application server.
6. Production validation should include first-time-user scenarios and clean browser state.
7. Small, controlled experiments can sometimes resolve uncertainty faster than extended analysis.
8. Operational documentation should evolve alongside the application rather than being created after development is complete.

## Deferred Items for Sprint 2

- Resolve remaining nullable reference warnings.
- Remove the unused exception variable in `WeatherDashboard`.
- Evaluate the Blazor WebAssembly `wasm-tools` workload.
- Evaluate improvements to Movie Favorites state management and caching.
- Continue application quality and performance improvements.
- Review portfolio presentation and user experience improvements.

## Sprint 1 Final Status

**Status:** Complete

Sprint 1 established a verified production deployment baseline for BlazorExpo.

The application now has:

- A GitHub-controlled source and deployment workflow.
- Automated Netlify deployments.
- Secure TMDB API integration.
- A verified production deployment.
- A documented operational process.
- A documented engineering decision baseline.

BlazorExpo RC1 is ready to serve as the foundation for Sprint 2.

## Closing Reflection

Sprint 1 began as an effort to establish a reliable deployment path for BlazorExpo and evolved into a broader exercise in deployment architecture, security, configuration management, production validation, and operational documentation.

The most significant outcome was not simply getting the application deployed. The project now has a repeatable process for moving changes from local development through source control and into production with clear validation points at each stage.

The lessons and documentation produced during Sprint 1 provide a stronger foundation for future development and allow Sprint 2 to focus on improving the application rather than continuing to solve fundamental deployment and operational issues.
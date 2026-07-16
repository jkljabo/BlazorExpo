Phase 1 - Local Verification (Visual Studio)

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

Phase 2 - Repository Verification (GitHub)

Before touching Netlify, verify:

✓ latest commit visible

✓ netlify.toml matches baseline

✓ global.json matches baseline

✓ tmdb.js matches baseline

✓ Program.cs verified

✓ BlazorCodeChallenge.csproj verified

Everything should match our local repository.

	Results: Confirmed
		
Phase 2.5 - Netlify Compliance
	
1. 	netlify.toml
	Looks compliant.
	Open Question: Do we keep both [[edge_functions]] and export const config or only one?
	Results:
		Pending Verification
		
2. global.json
	Looks compliant.
	Results:
		We're intentionally allowing the SDK to roll forward.
        The project targets SDK 8.0.422 locally. Netlify is configured independently through DOTNET_VERSION to use a compatible .NET 8 SDK available in its build environment.

3. tmdb.js
	Not yet compliant
	Results:
		Pending Final Review
		
Phase 3 - Netlify Project Creation

1. Create a new project.
	Import an existing project
	Provider: GitHub
	Repository: BlazorExpo
	Branch: main
	
	Results: Pending verificationt.
		

Phase 4 - Build Settings

1. Team:
	Jabo's BlazorExpo
	
	Results:
		Change to 'Netlify Team' at a later date.
	
2. Project name:
	jasonlittle-dev
	
	Results:
		Open field, went with jasonlittle-dev but can change later.

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
	
	Results: Creation pending on verification.
	
Phase 5 - Environment Variables

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
	
Phase 6 - Configuration Files

Before deploying, verify these are committed:

1. netlify.toml
	We will verify this against our latest baseline.

2. global.json
	Verify it matches our agreed SDK strategy.

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
	
Phase 7 - First Deployment:

1. Watch the deployment log.

2. Checklist:
	Repository cloned
	.NET restored
	Project published
	Publish directory found
	Edge Function detected
	Deployment succeeds

	Note: This is where we'll use our Netlify connector to inspect the results if anything is unexpected.
	
	Results: Pending verification.
	
Phase 8 - Functional Testing

Test in this order:
	1. Home page
	2. Weather Dashboard
	3. Loan Shark
	4. FizzBuzz
	5. Movie Time
	6. Browser Console

	Note: Movie Time is last because it's our primary deployment validation.
	
	Results: Pending verificationt.
	
Phase 9 - Acceptance Criteria:

1. Sprint 1 is complete when:
	GitHub push triggers Netlify automatically
	Build succeeds
	Movie Time displays live movies
	Browser console shows no deployment-related errors
	Site is ready to share
	
	Results: Pending verification.
	
Before We Create the New Project:

1. I want us to verify one thing together.

Deployment Baseline Checklist:

We'll treat this as our "go/no-go" list:

Item									Status
Local Build								✅
Local Publish							✅
GitHub Connected						✅
Netlify Connected						✅
Deployment Plan							✅
Environment Variables					✅
Documentation							🟡
Edge Function Review					🟡
Working Tree Clean						❌


Build → Verify → Commit → Push → Deploy → Validate
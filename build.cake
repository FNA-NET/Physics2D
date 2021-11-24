//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var baseVersion = "1.0.0.";

var target = Argument("build-target", "Default");
var version = Argument("build-version", EnvironmentVariable("BUILD_NUMBER") ?? baseVersion + "1");
var configuration = Argument("build-configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

DotNetCoreMSBuildSettings dnMSBuildSettings;
DotNetCoreBuildSettings dnBuildSettings;
DotNetCorePackSettings dnPackSettings;

private void BuildDotnet(string filePath)
{
    DotNetCoreBuild(filePath, dnBuildSettings);
}

private void PackDotnet(string filePath)
{
    DotNetCorePack(filePath, dnPackSettings);
}

private void ParseVersion()
{
    if (!string.IsNullOrEmpty(EnvironmentVariable("GITHUB_ACTIONS")))
    {
        var gitRef = EnvironmentVariable("GITHUB_REF");

        if (gitRef.Contains("refs/tags/")) // There's a git release
        {
            version = gitRef.Replace("refs/tags/v", "").Trim();
        }
        else
        {
            version = baseVersion + EnvironmentVariable("GITHUB_RUN_NUMBER");

            var branch = EnvironmentVariable("GITHUB_REF");

            if (branch != " refs/heads/release")
                version = version + "-ci";
        }
    }
    else
    {
        var branch = EnvironmentVariable("BRANCH_NAME") ?? string.Empty;
        if (!branch.Contains("release"))
            version += "-ci";
    }

    Console.WriteLine("Version: " + version);
}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Prep")
    .Does(() =>
{
    ParseVersion();

    dnMSBuildSettings = new DotNetCoreMSBuildSettings();
    dnMSBuildSettings.WithProperty("Version", version);

    dnBuildSettings = new DotNetCoreBuildSettings();
    dnBuildSettings.Verbosity = DotNetCoreVerbosity.Minimal;
    dnBuildSettings.Configuration = configuration;
    dnBuildSettings.MSBuildSettings = dnMSBuildSettings;

    dnPackSettings = new DotNetCorePackSettings();
    dnPackSettings.MSBuildSettings = dnMSBuildSettings;
    dnPackSettings.Verbosity = DotNetCoreVerbosity.Minimal;
    dnPackSettings.Configuration = configuration;
});

Task("BuildLib")
    .IsDependentOn("Prep")
    .Does(() =>
{
    BuildDotnet("Physics2D/Physics2D.csproj");
});

Task("BuildBenchmarks")
    .IsDependentOn("Prep")
    .Does(() =>
{
    BuildDotnet("Physics2D.Benchmarks/Physics2D.Benchmarks.csproj");
});

Task("BuildAll")
    .IsDependentOn("BuildLib")
    .IsDependentOn("BuildBenchmarks");

Task("Test")
    .Does(() =>
{
    DotNetCoreTest("Physics2D.UnitTests/Physics2D.UnitTests.csproj", new DotNetCoreTestSettings() { Configuration = configuration, });
});

Task("Pack")
    .IsDependentOn("Prep")
    .Does(() =>
{
    PackDotnet("Physics2D/Physics2D.csproj");
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("BuildAll");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);

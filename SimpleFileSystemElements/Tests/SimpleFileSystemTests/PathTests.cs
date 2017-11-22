namespace SimpleFileSystemTests
{
    using FluentAssertions;
    using NUnit.Framework;
    using SimpleFileSystem;

    [TestFixture]

    public sealed class PathTests
    {
        [TestCase("/", "/", TestName = "Path should be initialized with root path")]
        public void Path_Should_Be_Initialized(string inputPath, string outputPath)
        {
            var path = new Path(inputPath);

            path.CurrentPath.Should().Be(outputPath);
        }

        [Ignore("Not now!")]
        [TestCase("/a/b/c/d", "../x", "/a/b/c/x", TestName = "Cd method should move path to other subdirectory (Acceptance Test)")]
        public void Cd_Method_Should_Move_Path(string initialPath, string relatitivePath, string outputPath)
        {
            Path path = new Path(initialPath);

            path.Cd(relatitivePath);

            path.CurrentPath.Should().Be(outputPath);
        }
    }
}

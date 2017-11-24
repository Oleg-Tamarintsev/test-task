namespace SimpleFileSystemTests
{
    using FluentAssertions;
    using NUnit.Framework;
    using SimpleFileSystem;
    using System;

    [TestFixture]

    public sealed class PathTests
    {
        [TestCase("/", "/", TestName = "Path should be initialized with root path")]
        [TestCase("/a", "/a", TestName = "Path should be initialized with single subdirictory")]
        [TestCase("/a/..", "/", TestName = "Path should be initialized with multiple subdirictories and with using parent directory alias")]
        public void Path_Should_Be_Initialized(string inputPath, string outputPath)
        {
            var path = new Path(inputPath);

            path.CurrentPath.Should().Be(outputPath);
        }

        [TestCase("a", TestName = "Path should not be initialized with relative path")]
        [TestCase("/..", TestName = "Path should not be initialized with upper then root path")]
        [TestCase("//a", TestName = "Path should not be initialized with missed subdirictory")]
        [TestCase("/a/b/", TestName = "Path should not be initialized with closing path delimiters")]
        [TestCase("/a/..b", TestName = "Path should not be initialized with missing path delimiters")]
        [TestCase("/$", TestName = "Path should not be initialized with invalid subdirictories")]
        public void Path_Should_Not_Be_Initialized(string inputPath)
        {
            Action initialization = () => new Path(inputPath);

            initialization
                .ShouldThrow<InvalidPathException>();
        }

        [TestCase("/a/b/c/d", "../x", "/a/b/c/x", TestName = "Cd method should move path to other subdirectory (Acceptance Test)")]
        [TestCase("/", "a", "/a", TestName = "Cd method should move path to single subdirectory")]
        [TestCase("/a", "../a", "/a", TestName = "Cd method should move path to initial path in case 1")]
        [TestCase("/", "a/..", "/", TestName = "Cd method should move path to initial path in case 2")]
        public void Cd_Method_Should_Move_Path(string initialPath, string relatitivePath, string outputPath)
        {
            Path path = new Path(initialPath);

            path.Cd(relatitivePath);

            path.CurrentPath.Should().Be(outputPath);
        }

        [TestCase("/", "..", TestName = "Cd method should not move path to upper then root directory")]
        [TestCase("/", "/a", TestName = "Cd method should not move path to absolute path")]
        [TestCase("/", "a//b", TestName = "Cd method should not move path to badly qulified reletive path case 1")]
        [TestCase("/", "/a/b/", TestName = "Cd method should not move path to badly qulified reletive path case 2")]
        [TestCase("/", "a/..b", TestName = "Cd method should not move path to reletive path with unexpected symbols case 1")]
        [TestCase("/", "a/$", TestName = "Cd method should not move path to reletive path with unexpected symbols case 2")]
        public void Cd_Method_Should_Not_Move_Path(string initialPath, string relatitivePath)
        {
            Path path = new Path(initialPath);

            Action movemnet = () => path.Cd(relatitivePath);

            movemnet
                .ShouldThrow<InvalidPathException>();
        }
    }
}

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
        [TestCase("/a/b/c", "/a/b/c", TestName = "Path should be initialized with multiple subdirictories")]
        [TestCase("/a/../c", "/c", TestName = "Path should be initialized with multiple subdirictories and with using parent directory alias")]
        public void Path_Should_Be_Initialized(string inputPath, string outputPath)
        {
            var path = new Path(inputPath);

            path.CurrentPath.Should().Be(outputPath);
        }

        [TestCase("a", "Cannot build Path object based on relative path", TestName = "Path should not be initialized with relative path")]
        [TestCase("/..", "Cannot build Path object based on path upper to root directory", TestName = "Path should not be initialized with upper then root path")]
        [TestCase("//a", "Cannot build Path object because of subdirectory is missing", TestName = "Path should not be initialized with missed subdirictory")]
        [TestCase("/a/b/", "Cannot build Path object because of subdirectory is missing", TestName = "Path should not be initialized with closing path delimiters")]
        [TestCase("/a/..b", "Cannot build Path object because subdiricotry contains unexpected symbol", TestName = "Path should not be initialized with missing path delimiters")]
        [TestCase("/$", "Cannot build Path object because subdiricotry contains unexpected symbol", TestName = "Path should not be initialized with invalid subdirictories")]
        public void Path_Should_Not_Be_Initialized(string inputPath, string outputErrorMessage)
        {
            Action initialization = () => new Path(inputPath);

            initialization
                .ShouldThrow<InvalidPathException>()
                .WithMessage(outputErrorMessage);
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

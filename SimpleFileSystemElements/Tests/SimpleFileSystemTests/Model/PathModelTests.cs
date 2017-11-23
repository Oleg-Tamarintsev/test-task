namespace SimpleFileSystemTests.Model
{
    using FluentAssertions;
    using NUnit.Framework;
    using SimpleFileSystem;
    using SimpleFileSystem.Model;
    using System;

    [TestFixture]
    public sealed class PathModelTests
    {
        private AbsolutePathModel CreateAbsolutePathModel()
        {
            return new AbsolutePathModel();
        }

        private RelativePathModel CreateRelativePathModel()
        {
            return new RelativePathModel();
        }

        [Test]
        public void AbsolutePathModel_Sould_Add_Subdirectory()
        {
            var model = CreateAbsolutePathModel();

            model.AddSubdirectory("a");

            model.ToString().Should().Be("/a");
        }

        [Test]
        public void AbsolutePathModel_Sould_Add_Subdirectory_And_ParenDirectory()
        {
            var model = CreateAbsolutePathModel();

            model.AddSubdirectory("a");

            model.MoveToParentDirectory();

            model.ToString().Should().Be("/");
        }

        [Test]
        public void AbsolutePathModel_Sould_Not_Add_ParenDirectory()
        {
            var model = CreateAbsolutePathModel();

            Action moveToParent = () => model.MoveToParentDirectory();

            moveToParent.ShouldThrow<InvalidPathException>();
        }

        [Test]
        public void RelativePathModel_Sould_Add_Subdirectory()
        {
            var model = CreateRelativePathModel();

            model.AddSubdirectory("a");

            model.ToString().Should().Be("a");
        }

        [Test]
        public void RelativePathModel_Sould_Add_Subdirectory_And_ParenDirectory()
        {
            var model = CreateRelativePathModel();

            model.AddSubdirectory("a");

            model.MoveToParentDirectory();

            model.ToString().Should().Be("");
        }

        [Test]
        public void RelativePathModel_Sould_Add_ParenDirectory()
        {
            var model = CreateRelativePathModel();

            model.MoveToParentDirectory();

            model.ToString().Should().Be("..");
        }

        [Test]
        public void RelativePathModel_Sould_Add_ParenDirectory_And_Directory__Add_ParenDirectory()
        {
            var model = CreateRelativePathModel();

            model.MoveToParentDirectory();

            model.AddSubdirectory("a");

            model.MoveToParentDirectory();

            model.ToString().Should().Be("..");
        }
    }
}

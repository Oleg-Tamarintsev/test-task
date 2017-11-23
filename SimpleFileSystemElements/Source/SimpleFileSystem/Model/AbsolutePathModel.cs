namespace SimpleFileSystem.Model
{
    using System.Collections.Generic;

    public sealed class AbsolutePathModel : IPathModel
    {
        private List<string> _subdirectories = new List<string>();

        public void AddSubdirectory(string subdirectory)
        {
            _subdirectories.Add(subdirectory);
        }

        public void Cd(RelativePathModel relative)
        {
            relative.DoCd(this);
        }

        public void MoveToParentDirectory()
        {
            if(_subdirectories.Count == 0)
            {
                throw new InvalidPathException("Cannot build absolute path upper to root directory");
            }
            _subdirectories.RemoveAt(_subdirectories.Count - 1);
        }

        public override string ToString()
            => $"{SimpleFileSystemEnvironment.RootPath}{string.Join(SimpleFileSystemEnvironment.PathDelimiter.ToString(), _subdirectories)}";
    }
}

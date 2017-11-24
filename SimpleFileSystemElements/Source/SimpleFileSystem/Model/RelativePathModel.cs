namespace SimpleFileSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RelativePathModel : IPathModel
    {
        private readonly List<string> _subdirectories = new List<string>();

        public void AddSubdirectory(string subdirectory)
        {
            if (string.IsNullOrEmpty(subdirectory))
            {
                throw new ArgumentException(nameof(subdirectory));
            }
            _subdirectories.Add(subdirectory);
        }

        public void Cd(RelativePathModel relative)
        {
            DoCd(this);
        }

        public void MoveToParentDirectory()
        {
            if(IsLastDirictoryNotParent())
            {
                _subdirectories.RemoveAt(_subdirectories.Count - 1);
            } else
            {
                AddSubdirectory(SimpleFileSystemEnvironment.ParentDirectoryAlias);
            }
        }

        internal void DoCd(IPathModel pathModel)
        {
            foreach(var subdirectory in _subdirectories)
            {
                if(subdirectory == SimpleFileSystemEnvironment.ParentDirectoryAlias)
                {
                    pathModel.MoveToParentDirectory();
                }
                else
                {
                    pathModel.AddSubdirectory(subdirectory);
                }
            }
        }

        private bool IsLastDirictoryNotParent()
        {
            var lastSubdirectory = _subdirectories.LastOrDefault();
            return lastSubdirectory != null && lastSubdirectory != SimpleFileSystemEnvironment.ParentDirectoryAlias;
        }

        public override string ToString()
            => $"{string.Join(SimpleFileSystemEnvironment.PathDelimiter.ToString(), _subdirectories)}";
    }
}

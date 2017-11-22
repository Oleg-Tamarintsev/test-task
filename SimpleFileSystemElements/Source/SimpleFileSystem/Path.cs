namespace SimpleFileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Path
    {
        private List<string> _subdirectories;

        public Path(string inputPath)
        {
            _subdirectories = inputPath.Split('/').Skip(1).ToList();
        }

        public void Cd(string inputPath)
        {
            throw new NotImplementedException();
        }

        public string CurrentPath
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in _subdirectories)
                {
                    sb.Append('/');
                    sb.Append(item);
                }
                return sb.ToString();
            }
        }
    }
}

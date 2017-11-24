namespace SimpleFileSystem.Services
{
    using Model;
    using System;

    public sealed class PathBuilder
    {
        private IPathModel _model;

        public void CreateAbsolutePath()
        {
            if (_model == null)
            {
                _model = new AbsolutePathModel();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void CreateRelativePath()
        {
            if (_model == null)
            {
                _model = new RelativePathModel();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void AddSubdirectory(string subdirectory)
        {
            if (_model == null)
            {
                throw new InvalidOperationException();
            }
            _model.AddSubdirectory(subdirectory);
        }

        public void AddParentDirectory()
        {
            if (_model == null)
            {
                throw new InvalidOperationException();
            }
            _model.MoveToParentDirectory();
        }

        public IPathModel Build()
        {
            if(_model == null)
            {
                throw new InvalidOperationException();
            }
            return _model;
        }
    }
}
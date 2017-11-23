namespace SimpleFileSystem
{
    using Model;
    using Services;

    public sealed class Path
    {
        private IPathModel _model;

        private IPathModel GetPathModel(string input)
        {
            var factory = new PathFactory();
            return factory.Build(input);
        }

        public Path(string inputPath)
        {
            _model = GetPathModel(inputPath);
            if(!(_model is AbsolutePathModel))
            {
                throw new InvalidPathException("Cannot build Path object based on relative path");
            }
        }

        public void Cd(string inputPath)
        {
            var relative = GetPathModel(inputPath);
            if (relative is AbsolutePathModel)
            {
                throw new InvalidPathException("Cannot move to absolut path");
                //_model = relative;
            }
            if(relative is RelativePathModel)
            {
                _model.Cd(relative as RelativePathModel);
            }
        }

        public string CurrentPath
        {
            get
            {
                return _model.ToString();
            }
        }
    }
}

namespace SimpleFileSystem.Services
{
    using System;
    using Grammar.Parser;
    using Grammar.Lexer;
    using Model;

    public sealed class PathFactory: IPathBuilder
    {
        private readonly PathLexer _lexer = new PathLexer();
        private readonly PathParser _parser = new PathParser();
        private IPathModel _model;

        void IPathBuilder.AddParentDirectory()
        {
            if (_model == null)
            {
                _model = new RelativePathModel();
            }
            _model.MoveToParentDirectory();
        }

        void IPathBuilder.CreateRootDirectory()
        {
            if(_model == null)
            {
                _model = new AbsolutePathModel();
            }
            else
            {
                throw new InvalidOperationException("Bad command!");
            }
        }

        void IPathBuilder.AddSubdirectory(string subdirectory)
        {
            if (_model == null)
            {
                _model = new RelativePathModel();
            }
            _model.AddSubdirectory(subdirectory);
        }

        public IPathModel Build(string input)
        {
            if(_model != null)
            {
                return _model;
            }
            var tokens = _lexer.Tokenize(input);
            try
            {
                _parser.Parse(tokens, new ACLTokenAdapter(this));
            } catch(InvalidOperationException exception)
            {
                throw new InvalidPathException(exception.Message, exception);
            }
            return _model;
        }
    }
}

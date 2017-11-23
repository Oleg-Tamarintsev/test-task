namespace SimpleFileSystem.Services
{
    using System;
    using Grammar.Parser;
    using Grammar.Lexer;
    using Model;

    public sealed class PathFactory: IPathSegmentVisitor
    {
        private readonly PathLexer _lexer = new PathLexer();
        private readonly PathParser _parser = new PathParser();
        private IPathModel _model;

        public void AcceptParentDirectory()
        {
            if (_model == null)
            {
                _model = new RelativePathModel();
            }
            _model.MoveToParentDirectory();
        }

        public void AcceptRootDirectory()
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

        public void AcceptSubdirectory(string subdirectory)
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
            _parser.Parse(tokens, new ACLTokenVisitorAdapter(this));
            return _model;
        }
    }
}

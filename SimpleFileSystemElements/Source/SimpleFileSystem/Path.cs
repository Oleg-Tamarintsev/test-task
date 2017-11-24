namespace SimpleFileSystem
{
    using Antlr4.Runtime;
    using Grammar;
    using Model;
    using Services;

    public sealed class Path
    {
        private IPathModel _model;

        private IPathModel GetPathModel(string input)
        {
            var modelBuilder = new PathBuilder();

            var lexer = new PathGrammarLexer(new AntlrInputStream(input));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new PathParsingErrorListener());

            var tokens = new CommonTokenStream(lexer);
            var parser = new PathGrammarParser(tokens);

            parser.AddParseListener(new PathGrammarListener(modelBuilder));

            parser.RemoveErrorListeners();
            parser.AddErrorListener(new PathParsingErrorListener());

            var path = parser.path();

            return modelBuilder.Build();
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

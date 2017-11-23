namespace SimpleFileSystem.Grammar.Lexer
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class PathLexer
    {
        public PathLexer()
        {
            Init();
        }

        public IEnumerable<PathToken> Tokenize(string input)
        {
            var tokenContext = new PathLexerContext
            {
                StartPosition = 0,
                EndPosition = 0,
                InputString = input
            };
            yield return FromContext(
                        input,
                        PathTokenType.StartToken,
                        tokenContext);
            foreach (var symbol in input ?? string.Empty)
            {
                tokenContext.EndPosition += 1;
                var tokenDefinition = definitions.FirstOrDefault(v => v.IsMatched(tokenContext));
                if(tokenDefinition != null)
                {
                    yield return FromContext(
                        input,
                        tokenDefinition.Type,
                        tokenContext);
                    tokenContext.StartPosition = tokenContext.EndPosition;
                }
            }
            if( tokenContext.StartPosition != tokenContext.EndPosition)
            {
                yield return FromContext(
                       input,
                       PathTokenType.InvalidSequence,
                       tokenContext);
            }
            else
            {
                yield return FromContext(
                        input,
                        PathTokenType.EndToken,
                        tokenContext);
            }
        }

        private PathToken FromContext(string input, PathTokenType type, PathLexerContext tokenContext) 
            => new PathToken(
                        input,
                        input.Substring(tokenContext.StartPosition, tokenContext.EndPosition - tokenContext.StartPosition),
                        type,
                        tokenContext.StartPosition,
                        tokenContext.EndPosition);

        private void Init()
        {
            definitions.Add(new PathTokenDefinition(PathTokenType.RootDirectory, PathTokenDefinitionMattching.IsRootDirectoryMattching));
            definitions.Add(new PathTokenDefinition(PathTokenType.PathDelimiter, PathTokenDefinitionMattching.IsPathDelimiterMattching));
            definitions.Add(new PathTokenDefinition(PathTokenType.Subdirectory, PathTokenDefinitionMattching.IsSubdirectoryMattching));
            definitions.Add(new PathTokenDefinition(PathTokenType.ParentDirectory, PathTokenDefinitionMattching.IsParentDirectoryMattching));
        }

        private List<PathTokenDefinition> definitions = new List<PathTokenDefinition>();
    }
}

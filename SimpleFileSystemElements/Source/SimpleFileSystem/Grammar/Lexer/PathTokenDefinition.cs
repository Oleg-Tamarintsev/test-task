namespace SimpleFileSystem.Grammar.Lexer
{
    using System;

    public sealed class PathTokenDefinition
    {
        public PathTokenType Type { get; }

        public PathTokenDefinition(PathTokenType type, Predicate<PathLexerContext> matching)
        {
            if (matching == null)
            {
                throw new ArgumentNullException(nameof(matching));
            }
            _matching = matching;
            Type = type;
        }

        public bool IsMatched(PathLexerContext context) => _matching(context);

        private readonly Predicate<PathLexerContext> _matching;
    }
}

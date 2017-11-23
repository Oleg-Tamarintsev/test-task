namespace SimpleFileSystem.Grammar.Parser
{
    using Lexer;
    using System;

    public abstract class ParserState
    {
        public static ParserState PrepareToParse()
        {
            return new PrepareToParseState();
        }

        public bool IsParsingSuccesesful()
        {
            return this is ParsingComplete;
        }

        public abstract ParserState Accept(PathToken token);

        private sealed class PrepareToParseState : ParserState
        {
            public override ParserState Accept(PathToken token)
            {
                switch (token.Type)
                {
                    case PathTokenType.StartToken:
                        return new StartParseState();
                    default:
                        throw new InvalidOperationException("Unexpected token");
                }
            }
        }

        private sealed class StartParseState : ParserState
        {
            public override ParserState Accept(PathToken token)
            {
                switch (token.Type)
                {
                    case PathTokenType.ParentDirectory:
                    case PathTokenType.Subdirectory:
                        return new EnterToDirectoryState();
                    case PathTokenType.RootDirectory:
                        return new EnterRootDirectoryState();
                    default:
                        throw new InvalidOperationException("Unexpected token");
                }
            }
        }

        private sealed class ParsingComplete : ParserState
        {
            public override ParserState Accept(PathToken token)
            {
                throw new InvalidOperationException("Unexpected token");
            }
        }

        private sealed class OutOfDirectoryState : ParserState
        {
            public override ParserState Accept(PathToken token)
            {
                switch (token.Type)
                {
                    case PathTokenType.ParentDirectory:
                    case PathTokenType.Subdirectory:
                        return new EnterToDirectoryState();
                    case PathTokenType.EndToken:
                        throw new InvalidOperationException("Path cannot end with path delimiter");
                    case PathTokenType.PathDelimiter:
                        throw new InvalidOperationException("Invalid path: subdirectory is missing");
                    default:
                        throw new InvalidOperationException("Unexpected token");
                }
            }
        }

        private sealed class EnterToDirectoryState : ParserState
        {
            public override ParserState Accept(PathToken token)
            {
                switch(token.Type)
                {
                    case PathTokenType.EndToken:
                        return new ParsingComplete();
                    case PathTokenType.PathDelimiter:
                        return new OutOfDirectoryState();
                    case PathTokenType.Subdirectory:
                        throw new InvalidOperationException("Invalid path: subdiricotry contains unexpected symbol");
                    default:
                        throw new InvalidOperationException("Unexpected token");
                }
            }
        }

        private sealed class EnterRootDirectoryState : ParserState
        {
            public override ParserState Accept(PathToken token)
            {
                switch (token.Type)
                {
                    case PathTokenType.ParentDirectory:
                    case PathTokenType.Subdirectory:
                        return new EnterToDirectoryState();
                    case PathTokenType.EndToken:
                        return new ParsingComplete();
                    case PathTokenType.PathDelimiter:
                        throw new InvalidOperationException("Invalid path: subdirectory is missing");
                    default:
                        throw new InvalidOperationException("Unexpected token");
                }
            }
        }
    }
}
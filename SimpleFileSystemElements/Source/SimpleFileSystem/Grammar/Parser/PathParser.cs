namespace SimpleFileSystem.Grammar.Parser
{
    using Lexer;
    using System.Collections.Generic;
    using System;

    public sealed class PathParser
    {
        public void Parse(IEnumerable<PathToken> tokens, ITokenVisitor visitor)
        {
            if(visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }
            if(tokens == null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }
            var parceState = ParserState.PrepareToParse();
            foreach (var token in tokens)
            {
                parceState = parceState.Accept(token);
                visitor.Accept(token);
            }
            if(!parceState.IsParsingSuccesesful())
            {
                throw new InvalidOperationException("Token sequens is incompleted");
            }
        }
    }
}

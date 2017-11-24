namespace SimpleFileSystem.Services
{
    using Antlr4.Runtime;
    using System;

    public class PathParsingErrorListener : BaseErrorListener, IAntlrErrorListener<int>
    {
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new InvalidPathException($"Invalid Expression: {msg}", e);
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new InvalidPathException($"Invalid Expression: {msg}", e);
        }
    }
}

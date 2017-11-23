namespace SimpleFileSystem.Grammar.Lexer
{
    public struct PathLexerContext
    {
        public int StartPosition { get; set; }

        public int EndPosition { get; set; }

        public string InputString { get; set; }

        public int GetSymbolsCount()
        {
            return EndPosition - StartPosition;
        }
    }
}

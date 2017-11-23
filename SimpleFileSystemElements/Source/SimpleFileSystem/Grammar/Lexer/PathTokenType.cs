namespace SimpleFileSystem.Grammar.Lexer
{
    public enum PathTokenType
    {
        StartToken,
        RootDirectory,
        PathDelimiter,
        Subdirectory,
        ParentDirectory,
        InvalidSequence,
        EndToken
    }
}

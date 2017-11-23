namespace SimpleFileSystem.Grammar.Lexer
{
    using System.Linq;

    public static class PathTokenDefinitionMattching // ANTLR4 лучше решение
    {
        public static bool IsRootDirectoryMattching(PathLexerContext context)
        {
            return context.StartPosition == 0 &&
                AreEqual(SimpleFileSystemEnvironment.RootPath, context);
        }

        public static bool IsPathDelimiterMattching(PathLexerContext context)
        {
            return !IsRootDirectoryMattching(context) &&
                context.GetSymbolsCount() == 1 &&
                context.InputString[context.StartPosition] == SimpleFileSystemEnvironment.PathDelimiter;
        }

        public static bool IsSubdirectoryMattching(PathLexerContext context)
        {
            return
                (context.EndPosition == context.InputString.Length && IsValidSubdirectory(context)) ||
                (context.EndPosition != context.InputString.Length && IsPathDelimiterNext(context) && IsValidSubdirectory(context));

        }

        public static bool IsParentDirectoryMattching(PathLexerContext context)
        {
            return AreEqual(SimpleFileSystemEnvironment.ParentDirectoryAlias, context);
        }

        private static bool AreEqual(string pattern, PathLexerContext context)
        {
            return pattern.Length == context.GetSymbolsCount() &&
                Enumerable.SequenceEqual(pattern, context.InputString.Skip(context.StartPosition).Take(context.GetSymbolsCount()));
        }

        private static bool IsValidSubdirectory(PathLexerContext context)
        {
            return SimpleFileSystemEnvironment.IsValidSubdirectory(context.InputString.Skip(context.StartPosition).Take(context.GetSymbolsCount()));
        }

        private static bool IsPathDelimiterNext(PathLexerContext context)
        {
            return IsPathDelimiterMattching(new PathLexerContext
            {
                StartPosition = context.EndPosition,
                EndPosition = context.EndPosition + 1,
                InputString = context.InputString
            });
        }
    }
}

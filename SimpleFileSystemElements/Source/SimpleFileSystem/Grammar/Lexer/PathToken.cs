namespace SimpleFileSystem.Grammar.Lexer
{
    using System;

    public sealed class PathToken
    {
        public PathToken(string inputString, string value, PathTokenType type, int startIndex, int endIndex)
        {
            if(string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentException(nameof(inputString));
            }
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if(0 > startIndex || startIndex > inputString.Length)
            {
                throw new ArgumentException(nameof(startIndex));
            }
            if (0 > endIndex || endIndex > inputString.Length || startIndex > endIndex)
            {
                throw new ArgumentException(nameof(startIndex));
            }
            InputString = inputString;
            Value = value;
            Type = type;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public PathTokenType Type { get; }

        public string Value { get; }

        public string InputString { get; }

        public int StartIndex { get; }

        public int EndIndex { get; }
    }
}

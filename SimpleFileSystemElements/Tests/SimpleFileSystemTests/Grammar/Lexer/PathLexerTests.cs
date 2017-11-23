namespace SimpleFileSystemTests.Grammar.Lexer
{
    using FluentAssertions;
    using NUnit.Framework;
    using SimpleFileSystem.Grammar.Lexer;
    using System.Collections;

    [TestFixture]

    public sealed class PathLexerTests
    {
        public class PathLexerTestCasesFactory
        {
            const string testCase1InputString = "/";
            const string testCase2InputString = "a/";
            const string testCase3InputString = "/..b/$a/b/";

            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(testCase1InputString, new[]
                        {
                            new PathToken(testCase1InputString, string.Empty, PathTokenType.StartToken, 0, 0),
                            new PathToken(testCase1InputString, "/", PathTokenType.RootDirectory, 0, 1),
                            new PathToken(testCase1InputString, string.Empty, PathTokenType.EndToken, 1, 1)
                        }).SetName("Lexer should tokenize root path");


                    yield return new TestCaseData(testCase2InputString, new[]
                        {
                            new PathToken(testCase2InputString, string.Empty, PathTokenType.StartToken, 0, 0),
                            new PathToken(testCase2InputString, "a", PathTokenType.Subdirectory, 0, 1),
                            new PathToken(testCase2InputString, "/", PathTokenType.PathDelimiter, 1, 2),
                            new PathToken(testCase2InputString, string.Empty, PathTokenType.EndToken, 2, 2)
                        }).SetName("Lexer should tokenize relative path");

                    yield return new TestCaseData(testCase3InputString, new[]
                        {
                            new PathToken(testCase3InputString, string.Empty, PathTokenType.StartToken, 0, 0),
                            new PathToken(testCase3InputString, "/", PathTokenType.RootDirectory, 0, 1),
                            new PathToken(testCase3InputString, "..", PathTokenType.ParentDirectory, 1, 3),
                            new PathToken(testCase3InputString, "b", PathTokenType.Subdirectory, 3, 4),
                            new PathToken(testCase3InputString, "/", PathTokenType.PathDelimiter, 4, 5),
                            new PathToken(testCase3InputString, "$a/b/", PathTokenType.InvalidSequence, 5, 10)
                        }).SetName("Lexer should tokenize multiple subdirictories even with error");
                }
            }
        }

        private PathLexer _pathLexer;

        [SetUp]
        public void Init()
        {
            _pathLexer = new PathLexer();
        }

        [Test, TestCaseSource(typeof(PathLexerTestCasesFactory), "TestCases")]
        public void Lexer_Should_Tokenize_Input(string input, PathToken[] expectedTokens)
        {
            var tokens = _pathLexer.Tokenize(input);
            tokens.Should().Equal(expectedTokens, 
                (t, e) => 
                    t.Type == e.Type &&
                    t.Value == e.Value &&
                    t.InputString == e.InputString &&
                    t.StartIndex == e.StartIndex &&
                    t.EndIndex == e.EndIndex);
        }
    }
}
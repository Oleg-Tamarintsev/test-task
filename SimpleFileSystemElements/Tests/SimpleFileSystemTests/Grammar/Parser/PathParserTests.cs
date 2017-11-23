namespace SimpleFileSystemTests.Grammar
{
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using SimpleFileSystem.Grammar.Lexer;
    using SimpleFileSystem.Grammar.Parser;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [TestFixture]
    public sealed class PathParserTests
    {
        public class ParserTestCasesFactory
        {
            const string testCase1InputString = "/";

            public static IEnumerable ValidTokenSequenceTestCases
            {
                get
                {
                    yield return new TestCaseData((object)new[]
                        {
                            new PathToken(testCase1InputString, string.Empty, PathTokenType.StartToken, 0, 0),
                            new PathToken(testCase1InputString, "/", PathTokenType.RootDirectory, 0, 1),
                            new PathToken(testCase1InputString, string.Empty, PathTokenType.EndToken, 1, 1)
                        }).SetName("Parser should parse valid sequence");
                }
            }

            public static IEnumerable InvalidTokenSequenceTestCases
            {
                get
                {
                    yield return new TestCaseData((object)new[]
                        {
                            new PathToken(testCase1InputString, "/", PathTokenType.RootDirectory, 0, 1),
                            new PathToken(testCase1InputString, string.Empty, PathTokenType.EndToken, 1, 1)
                        }).SetName("Parser should not parse invalid sequence");

                    yield return new TestCaseData((object)new[]
                        {
                            new PathToken(testCase1InputString, string.Empty, PathTokenType.StartToken, 0, 0),
                            new PathToken(testCase1InputString, "/", PathTokenType.RootDirectory, 0, 1)
                        }).SetName("Parser should not parse invalid sequence");
                }
            }
        }

        private PathParser _patParser;
        private Mock<ITokenVisitor> _builder;

        [SetUp]
        public void Init()
        {
            _patParser = new PathParser();
            _builder = new Mock<ITokenVisitor>();
        }

        [Test, TestCaseSource(typeof(ParserTestCasesFactory), "ValidTokenSequenceTestCases")]
        public void Parser_Should_Parse_Valid_Sequence(PathToken[] tokens)
        {
            var processedTokens = new List<PathToken>();
            _builder.Setup(v => v.Accept(It.IsAny<PathToken>())).Callback((PathToken token) => processedTokens.Add(token));
            _patParser.Parse(tokens, _builder.Object);

            processedTokens.Should().Equal(tokens,
                (t, e) =>
                    t.Type == e.Type &&
                    t.Value == e.Value &&
                    t.InputString == e.InputString &&
                    t.StartIndex == e.StartIndex &&
                    t.EndIndex == e.EndIndex);
        }

        [Test, TestCaseSource(typeof(ParserTestCasesFactory), "InvalidTokenSequenceTestCases")]
        public void Parser_Should_Not_Parse_Invalid_Sequence(PathToken[] tokens)
        {
            Action parsing = () => _patParser.Parse(tokens, _builder.Object);

            parsing
                .ShouldThrow<InvalidOperationException>();
        }
    }
}

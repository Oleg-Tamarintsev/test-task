using SimpleFileSystem.Grammar.Lexer;

namespace SimpleFileSystem.Grammar.Parser
{
    public interface ITokenVisitor
    {
        void Accept(PathToken token);
    }
}

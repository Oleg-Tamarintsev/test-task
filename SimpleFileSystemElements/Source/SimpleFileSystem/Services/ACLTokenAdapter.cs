namespace SimpleFileSystem.Services
{
    using System;
    using Grammar.Lexer;
    using Grammar.Parser;

    public class ACLTokenAdapter : ITokenVisitor
    {
        private readonly IPathBuilder _visitor;

        public ACLTokenAdapter(IPathBuilder visitor)
        {
            if(visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }
            _visitor = visitor;
        }

        public void Accept(PathToken token)
        {
            switch(token.Type)
            {
                case PathTokenType.Subdirectory:
                    _visitor.AddSubdirectory(token.Value);
                    break;
                case PathTokenType.RootDirectory:
                    _visitor.CreateRootDirectory();
                    break;
                case PathTokenType.ParentDirectory:
                    _visitor.AddParentDirectory();
                    break;
            }
        }
    }
}

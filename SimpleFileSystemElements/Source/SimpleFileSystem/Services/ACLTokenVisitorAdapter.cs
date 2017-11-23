namespace SimpleFileSystem.Services
{
    using System;
    using Grammar.Lexer;
    using Grammar.Parser;

    public class ACLTokenVisitorAdapter : ITokenVisitor
    {
        private readonly IPathSegmentVisitor _visitor;

        public ACLTokenVisitorAdapter(IPathSegmentVisitor visitor)
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
                    _visitor.AcceptSubdirectory(token.Value);
                    break;
                case PathTokenType.RootDirectory:
                    _visitor.AcceptRootDirectory();
                    break;
                case PathTokenType.ParentDirectory:
                    _visitor.AcceptParentDirectory();
                    break;
            }
        }
    }
}

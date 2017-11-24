namespace SimpleFileSystem.Services
{
    using System;
    using Antlr4.Runtime.Misc;
    using Grammar;

    public class PathGrammarListener : PathGrammarBaseListener
    {
        private readonly PathBuilder _builder;

        public PathGrammarListener(PathBuilder builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            _builder = builder;
        }

        public override void EnterAbsolutePath(PathGrammarParser.AbsolutePathContext context)
        {
            _builder.CreateAbsolutePath();
        }

        public override void EnterRelativePath(PathGrammarParser.RelativePathContext context)
        {
            _builder.CreateRelativePath();
        }

        public override void ExitDirectory(PathGrammarParser.DirectoryContext context)
        {
            _builder.AddSubdirectory(context.GetText());
        }

        public override void ExitParentDirecoty(PathGrammarParser.ParentDirecotyContext context)
        {
            _builder.AddParentDirectory();
        }
    }
}

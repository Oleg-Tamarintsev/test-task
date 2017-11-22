namespace SimpleFileSystem
{
    using System;

    public sealed class InvalidPathException: ApplicationException
    {
        public InvalidPathException(string message) : base(message)
        {
        }
    }
}

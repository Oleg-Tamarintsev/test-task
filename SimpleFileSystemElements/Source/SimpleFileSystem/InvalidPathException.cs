namespace SimpleFileSystem
{
    using System;

    public sealed class InvalidPathException: ApplicationException
    {
        public InvalidPathException(string message) : base(message)
        {
        }

        public InvalidPathException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

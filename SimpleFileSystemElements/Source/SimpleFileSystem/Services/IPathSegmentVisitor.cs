namespace SimpleFileSystem.Services
{
    public interface IPathSegmentVisitor
    {
        void AcceptRootDirectory();

        void AcceptParentDirectory();

        void AcceptSubdirectory(string subdirectory);
    }
}

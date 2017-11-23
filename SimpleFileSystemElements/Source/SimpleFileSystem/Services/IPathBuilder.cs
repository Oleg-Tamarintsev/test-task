namespace SimpleFileSystem.Services
{
    public interface IPathBuilder
    {
        void AcceptRootDirectory();

        void AcceptParentDirectory();

        void AcceptSubdirectory(string subdirectory);
    }
}

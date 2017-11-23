namespace SimpleFileSystem.Services
{
    public interface IPathBuilder
    {
        void CreateRootDirectory();

        void AddParentDirectory();

        void AddSubdirectory(string subdirectory);
    }
}

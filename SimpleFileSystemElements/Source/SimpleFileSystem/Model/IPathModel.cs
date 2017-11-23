namespace SimpleFileSystem.Model
{
    public interface IPathModel
    {
        void AddSubdirectory(string subdirectory);

        void MoveToParentDirectory();

        void Cd(RelativePathModel relative);
    }
}

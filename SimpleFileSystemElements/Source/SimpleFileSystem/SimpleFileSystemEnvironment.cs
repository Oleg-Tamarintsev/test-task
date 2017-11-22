namespace SimpleFileSystem
{
    public static class SimpleFileSystemEnvironment
    {
        private const string DefaultRootPath = "/";
        private const char DefaultPathDelimiter = '/';
        private const string DefaultParentDirectoryAlias = "..";

        /// <summary>
        /// Возвращает строку, обозначающую в данной среде корневой путь
        /// </summary>
        public static string RootPath
        {
            get
            {
                return DefaultRootPath; // todo: ensure using oterwise remove me!
            }
        }

        /// <summary>
        /// Возвращает строку, обозначающую в данной среде разделитель пути
        /// </summary>
        public static char PathDelimiter
        {
            get
            {
                return DefaultPathDelimiter; // todo: ensure using oterwise remove me!
            }
        }

        /// <summary>
        /// Возвращает строку, обозначающую в данной среде сроку используемую для адресации на родительскую директорию
        /// </summary>
        public static string ParentDirectoryAlias
        {
            get
            {
                return DefaultParentDirectoryAlias;
            }
        }
    }
}

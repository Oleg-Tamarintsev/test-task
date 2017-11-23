namespace SimpleFileSystem
{
    using System.Collections.Generic;
    using System.Linq;

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
                return DefaultRootPath;
            }
        }

        /// <summary>
        /// Возвращает строку, обозначающую в данной среде разделитель пути
        /// </summary>
        public static char PathDelimiter
        {
            get
            {
                return DefaultPathDelimiter;
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

        /// <summary>
        /// Проверяет, что заданную последоватльность символов валидно называть поддиректорией в данной среде
        /// </summary>
        public static bool IsValidSubdirectory(IEnumerable<char> subdirectory)
        {
            return subdirectory.All(s => ('a' <= s && s <= 'z') || ('A' <= s && s <= 'Z')); // todo
        }
    }
}

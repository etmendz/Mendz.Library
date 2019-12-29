using System;
using System.IO;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// Provides methods to organize files in folders by date, year/month and year.
    /// </summary>
    public static class FileOrganizer
    {
        #region By file info
        /// <summary>
        /// Organizes a file by its creation date/time info.
        /// </summary>
        /// <param name="filename">The filename to organize.</param>
        /// <param name="targetPath">The target pathl</param>
        /// <param name="mode">The file organizer mode.</param>
        public static void Organize(string filename, string targetPath,
            FileOrganizerMode mode = FileOrganizerMode.Date) =>
            OrganizeHelper(filename, targetPath, File.GetCreationTime(filename), mode);

        /// <summary>
        /// Organizes a wildcard list of files by their creation date/time info.
        /// </summary>
        /// <param name="sourcePath">The filename to organize.</param>
        /// <param name="targetPath">The target pathl</param>
        /// <param name="wildcard">The wildcard or search pattern.</param>
        /// <param name="mode">The file organizer mode.</param>
        public static void OrganizeAll(string sourcePath, string targetPath, 
            string wildcard = "*.*", 
            FileOrganizerMode mode = FileOrganizerMode.Date)
        {
            foreach (var file in Directory.GetFiles(sourcePath, wildcard))
            {
                Organize(file, targetPath, mode);
            }
        }

        /// <summary>
        /// Organizes a wildcard list of files by their creation date/time info if the FileInfo condition is met.
        /// </summary>
        /// <param name="sourcePath">The filename to organize.</param>
        /// <param name="targetPath">The target pathl</param>
        /// <param name="condition">The FileInfo condition to meet/pass.</param>
        /// <param name="wildcard">The wildcard or search pattern.</param>
        /// <param name="mode">The file organizer mode.</param>
        public static void OrganizeAllWhen(string sourcePath, string targetPath, Func<FileInfo, bool> condition,
            string wildcard = "*.*",
            FileOrganizerMode mode = FileOrganizerMode.Date)
        {
            foreach (var file in Directory.GetFiles(sourcePath, wildcard))
            {
                if (condition(new FileInfo(file))) Organize(file, targetPath, mode);
            }
        }
        #endregion

        #region By (date or date/time) stamped filename
        /// <summary>
        /// Organizes a stamped file by its creation date/time info.
        /// </summary>
        /// <param name="stampedFileName">The stamped filename to organize.</param>
        /// <param name="targetPath">The target pathl</param>
        /// <param name="mode">The file organizer mode.</param>
        public static void Organize<T>(T stampedFileName, string targetPath, 
            FileOrganizerMode mode = FileOrganizerMode.Date)
            where T : StampedFileNameBase<DateTime>
        {
            if (stampedFileName == null) throw new ArgumentNullException(nameof(stampedFileName));
            OrganizeHelper(stampedFileName.ToString(), targetPath, stampedFileName.StampValue, mode);
        }

        /// <summary>
        /// Organizes a wildcard list of files by their date stamp value.
        /// </summary>
        /// <param name="sourcePath">The filename to organize.</param>
        /// <param name="targetPath">The target pathl</param>
        /// <param name="wildcard">The wildcard or search pattern.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="mode">The file organizer mode.</param>
        public static void OrganizeAllDateStampedFile(string sourcePath, string targetPath,
            string wildcard = "*.*", char separator = '_',
            FileOrganizerMode mode = FileOrganizerMode.Date)
        {
            foreach (var file in Directory.GetFiles(sourcePath, wildcard))
            {
                Organize(DateStampedFileName.Parse(file, separator), targetPath, mode);
            }
        }

        /// <summary>
        /// Organizes a wildcard list of files by their date/tim stamp value.
        /// </summary>
        /// <param name="sourcePath">The filename to organize.</param>
        /// <param name="targetPath">The target pathl</param>
        /// <param name="wildcard">The wildcard or search pattern.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="mode">The file organizer mode.</param>
        public static void OrganizeAllDateTimeStampedFile(string sourcePath, string targetPath, 
            string wildcard = "*.*", char separator = '_',
            FileOrganizerMode mode = FileOrganizerMode.Date)
        {
            foreach (var file in Directory.GetFiles(sourcePath, wildcard))
            {
                Organize(DateTimeStampedFileName.Parse(file, separator), targetPath, mode);
            }
        }
        #endregion

        /// <summary>
        /// A helper method to help organize files.
        /// </summary>
        /// <param name="filename">The file to organize.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="date">The date to use.</param>
        /// <param name="mode">The FileOrganizerMode to use. Default is FileOrganizerMode.Date.</param>
        private static void OrganizeHelper(string filename, string targetPath, DateTime date, 
            FileOrganizerMode mode = FileOrganizerMode.Date)
        {
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            File.Move(filename, Path.Combine(Path.Combine(targetPath, GetDirectory()), Path.GetFileName(filename)));
            string GetDirectory()
            {
                return mode switch
                {
                    FileOrganizerMode.Date => date.ToString("yyyyMMdd"),
                    FileOrganizerMode.YearMonth => date.ToString("yyyyMM"),
                    FileOrganizerMode.Year => date.Year.ToString(),
                    _ => date.ToString("yyyyMMdd")
                };
            }
        }
    }
}

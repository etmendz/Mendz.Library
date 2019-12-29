using System;
using System.Globalization;
using System.IO;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// Defines a date stamped filename.
    /// </summary>
    /// <remarks>
    /// The default stamp format is yyyyMMdd[_#], where # is the non-zero counter value.
    /// If stamped as extension, the format is <filename>[.ext].yyyyMMdd[_#].
    /// Else, the format is <filename>_yyyyMMdd[_#][.ext].
    /// </remarks>
    public class DateStampedFileName : StampedFileNameBase<DateTime>
    {
        public const string DefaultDateStampFormat = "yyyyMMdd";

        /// <summary>
        /// Gets the formatted date stamp.
        /// </summary>
        public string DateStamp => StampValue.ToString(DefaultDateStampFormat);

        /// <summary>
        /// Creates an instance of a DateStampedFileName.
        /// </summary>
        /// <param name="filename">The filename to stamp.</param>
        /// <param name="date">The date value.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="extension">Indicates if to stamp as extension or not. Default is false.</param>
        /// <param name="counter">The seed counter. Default is 0.</param>
        public DateStampedFileName(string filename, DateTime date, char separator = '_', bool extension = false, int counter = 0)
            : base(filename, date, separator, extension, counter) { }

        /// <summary>
        /// Creates an instance of a DateStampedFileName.
        /// </summary>
        /// <param name="filename">The filename to stamp.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="extension">Indicates if to stamp as extension or not. Default is false.</param>
        /// <param name="counter">The seed counter. Default is 0.</param>
        /// <remarks>
        /// The stamp value defaults to DateTime.Today.
        /// </remarks>
        public DateStampedFileName(string filename, char separator = '_', bool extension = false, int counter = 0)
            : this(filename, DateTime.Today, separator, extension, counter) { }

        /// <summary>
        /// Formats the stamp value, which is the DateStamp.
        /// </summary>
        /// <returns>The formatted date stamp value.</returns>
        protected override string FormatStampValue() => DateStamp;

        /// <summary>
        /// Deconstructs the stamped filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="date">The stamp value.</param>
        /// <param name="dateStamp">The date stamp.</param>
        /// <param name="counter">The counter.</param>
        public void Deconstruct(out string filename, out DateTime date, out string dateStamp, out int counter)
        {
            filename = FileName;
            date = StampValue;
            dateStamp = DateStamp;
            counter = Counter;
        }

        /// <summary>
        /// Parses a path to a DateStampedFileName.
        /// </summary>
        /// <param name="path">The path to parse.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>An instance of the DateStampedFileName for the given path and separator.</returns>
        public static DateStampedFileName Parse(string path, char separator = '_')
        {
            // Define the supported date stamp pattern
            string stampPattern = @"\d{8}(" + separator + @"[0-9]+)?";
            (bool extension, string filename, string[] stamp) = ParseHelper(path, stampPattern, separator);
            string dateStamp = stamp[0];
            DateTime date = DateTime.ParseExact(dateStamp, DefaultDateStampFormat, CultureInfo.InvariantCulture);
            // Parse out the counter, if any
            int counter = 0;
            if (stamp.Length == 2) counter = Convert.ToInt32(stamp[1]);
            // Put them all together...
            return new DateStampedFileName(Path.Combine(Path.GetDirectoryName(path), filename), date, separator, extension, counter);
        }
    }
}

using System;
using System.Globalization;
using System.IO;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// Defines a date/time stamped filename.
    /// </summary>
    /// <remarks>
    /// The default stamp format is yyyyMMdd_HHmmss[_#], where # is the non-zero counter value.
    /// If stamped as extension, the format is <filename>[.ext].yyyyMMdd_HHmmss[_#].
    /// Else, the format is <filename>_yyyyMMdd_HHmmss[_#][.ext].
    /// </remarks>
    public class DateTimeStampedFileName : StampedFileNameBase<DateTime>
    {
        public const string DefaultDateStampFormat = "yyyyMMdd";
        public const string DefaultTimeStampFormat = "HHmmss";

        /// <summary>
        /// Gets the formatted date stamp.
        /// </summary>
        public string DateStamp => StampValue.ToString(DefaultDateStampFormat);

        /// <summary>
        /// Gets the formatted time stamp.
        /// </summary>
        public string TimeStamp => StampValue.ToString(DefaultTimeStampFormat);

        /// <summary>
        /// Creates an instance of a DateTimeStampedFileName.
        /// </summary>
        /// <param name="filename">The filename to stamp.</param>
        /// <param name="dateTime">The date/time value.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="extension">Indicates if to stamp as extension or not. Default is false.</param>
        /// <param name="counter">The seed counter. Default is 0.</param>
        public DateTimeStampedFileName(string filename, DateTime dateTime, char separator = '_', bool extension = false, int counter = 0)
            : base(filename, dateTime, separator, extension, counter) { }

        /// <summary>
        /// Creates an instance of a DateTimeStampedFileName.
        /// </summary>
        /// <param name="filename">The filename to stamp.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="extension">Indicates if to stamp as extension or not. Default is false.</param>
        /// <param name="counter">The seed counter. Default is 0.</param>
        /// <remarks>
        /// The stamp value defaults to DateTime.Now.
        /// </remarks>
        public DateTimeStampedFileName(string filename, char separator = '_', bool extension = false, int counter = 0)
            : this(filename, DateTime.Now, separator, extension, counter) { }

        /// <summary>
        /// Formats the stamp value, which is a concatenation of DateStamp, Separator and TimeStamp.
        /// </summary>
        /// <returns>The formatted date/time stamp value.</returns>
        protected override string FormatStampValue() => DateStamp + Separator + TimeStamp;

        /// <summary>
        /// Deconstructs the stamped filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="dateTime">The stamp value.</param>
        /// <param name="dateStamp">The date stamp.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="counter">The counter.</param>
        public void Deconstruct(out string filename, out DateTime dateTime, out string dateStamp, out string timeStamp, out int counter)
        {
            filename = FileName;
            dateTime = StampValue;
            dateStamp = DateStamp;
            timeStamp = TimeStamp;
            counter = Counter;
        }

        /// <summary>
        /// Parses a path to a DateTimeStampedFileName.
        /// </summary>
        /// <param name="path">The path to parse.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>An instance of the DateTimeStampedFileName for the given path and separator.</returns>
        public static DateTimeStampedFileName Parse(string path, char separator = '_')
        {
            // Define the supported date/time stamp pattern
            string stampPattern = @"\d{8}" + separator + @"\d{6}" + @"(" + separator + @"[0-9]+)?";
            (bool extension, string filename, string[] stamp) = ParseHelper(path, stampPattern, separator);
            string dateStamp = stamp[0];
            string timeStamp = stamp[1];
            DateTime dateTime = DateTime.ParseExact(dateStamp + separator + timeStamp, 
                DefaultDateStampFormat + separator + DefaultTimeStampFormat, 
                CultureInfo.InvariantCulture);
            // Parse out the counter, if any
            int counter = 0;
            if (stamp.Length == 3) counter = Convert.ToInt32(stamp[2]);
            // Put them all together...
            return new DateTimeStampedFileName(Path.Combine(Path.GetDirectoryName(path), filename), dateTime, separator, extension, counter);
        }
    }
}

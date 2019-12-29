using System;
using System.IO;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// Defines a stamped filename.
    /// </summary>
    /// <remarks>
    /// The default stamp format is <stamp>[_#], where # is the non-zero counter value.
    /// If stamped as extension, the format is <filename>[.ext].<stamp>[_#].
    /// Else, the format is <filename>_<stamp>[_#][.ext].
    /// </remarks>
    public class StampedFileName : StampedFileNameBase<string>
    {
        /// <summary>
        /// Creates an instance of a StampedFileName.
        /// </summary>
        /// <param name="filename">The filename to stamp.</param>
        /// <param name="stampValue">The stamp value.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="extension">Indicates if to stamp as extension or not. Default is false.</param>
        /// <param name="counter">The seed counter. Default is 0.</param>
        public StampedFileName(string filename, string stampValue, char separator = '_', bool extension = false, int counter = 0)
            : base(filename, stampValue, separator, extension, counter) { }

        /// <summary>
        /// Parses a path to a StampedFileName.
        /// </summary>
        /// <param name="path">The path to parse.</param>
        /// <param name="stampValuePattern">The stamp value pattern.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>An instance of the StampedFileName for the given path and separator.</returns>
        public static StampedFileName Parse(string path, string stampValuePattern, char separator = '_')
        {
            // Define the supported date stamp pattern
            string stampPattern = stampValuePattern + @"(" + separator + @"[0-9]+)?";
            (bool extension, string filename, string[] stamp) = ParseHelper(path, stampPattern, separator);
            string stampValue = stamp[0];
            // Parse out the counter, if any
            int counter = 0;
            if (stamp.Length == 2) counter = Convert.ToInt32(stamp[1]);
            // Put them all together...
            return new StampedFileName(Path.Combine(Path.GetDirectoryName(path), filename), stampValue, separator, extension, counter);
        }
    }
}

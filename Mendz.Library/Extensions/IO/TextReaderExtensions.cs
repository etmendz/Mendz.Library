using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Mendz.Library.Extensions.IO
{
    /// <summary>
    /// Provides TextReader extensions.
    /// </summary>
    public static class TextReaderExtensions
    {
        /// <summary>
        /// Reads a line that matches a regular expression pattern.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>A line that matched the regular expression pattern.</returns>
        public static string ReadLineMatch(this TextReader reader, string pattern, RegexOptions options = RegexOptions.None)
        {
            string line = string.Empty;
            while (reader.Peek() > -1)
            {
                line = reader.ReadLine();
                if (line.IsMatch(pattern, options))
                {
                    break;
                }
            }
            return line;
        }

        /// <summary>
        /// Reads all lines that match a regular expression pattern.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>All lines that matched the regular expression pattern.</returns>
        public static string ReadAllMatch(this TextReader reader, string pattern, RegexOptions options = RegexOptions.None)
        {
            StringBuilder sb = new StringBuilder();
            string line;
            while (reader.Peek() > -1)
            {
                line = reader.ReadLine();
                if (line.IsMatch(pattern, options))
                {
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Yield lines from the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>Enumerable/iterable lines.</returns>
        public static IEnumerable<string> YieldLine(this TextReader reader)
        {
            while (reader.Peek() > -1)
            {
                yield return reader.ReadLine();
            }
        }

        /// <summary>
        /// Yield lines that match a regular expression pattern.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>Enumerable/iterable lines that matched the regular expression pattern.</returns>
        public static IEnumerable<string> YieldLineMatch(this TextReader reader, string pattern, RegexOptions options = RegexOptions.None)
        {
            string line;
            while (reader.Peek() > -1)
            {
                line = reader.ReadLine();
                if (line.IsMatch(pattern, options))
                {
                    yield return line;
                }
            }
        }
    }
}

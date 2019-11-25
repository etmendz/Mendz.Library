using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        public static string ReadLineMatch(this TextReader reader, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            string line = string.Empty;
            while (reader.Peek() > -1)
            {
                line = reader.ReadLine();
                if (line.IsMatch(pattern, options)) break;
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
        public static string ReadAllMatch(this TextReader reader, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            StringBuilder sb = new StringBuilder();
            foreach (var line in reader.YieldLineMatch(pattern, options))
            {
                sb.AppendLine(line);
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
            if (reader == null) throw new ArgumentNullException(nameof(reader));
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
        public static IEnumerable<string> YieldLineMatch(this TextReader reader, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            string line;
            while (reader.Peek() > -1)
            {
                line = reader.ReadLine();
                if (line.IsMatch(pattern, options)) yield return line;
            }
        }

        #region Async
        /// <summary>
        /// Asynchronously reads a line that matches a regular expression pattern.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>A line that matched the regular expression pattern.</returns>
        public static async Task<string> ReadLineMatchAsync(this TextReader reader, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            string line = string.Empty;
            while (reader.Peek() > -1)
            {
                line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line.IsMatch(pattern, options)) break;
            }
            return line;
        }

        /// <summary>
        /// Asynchronously reads all lines that match a regular expression pattern.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>All lines that matched the regular expression pattern.</returns>
        public static async Task<string> ReadAllMatchAsync(this TextReader reader, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            StringBuilder sb = new StringBuilder();
            await foreach (var line in reader.YieldLineMatchAsync(pattern, options))
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Asynchronously yield lines from the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>Enumerable/iterable lines.</returns>
        public static async IAsyncEnumerable<string> YieldLineAsync(this TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            while (reader.Peek() > -1)
            {
                yield return await reader.ReadLineAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Yield lines that match a regular expression pattern.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>Enumerable/iterable lines that matched the regular expression pattern.</returns>
        public static async IAsyncEnumerable<string> YieldLineMatchAsync(this TextReader reader, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            string line;
            while (reader.Peek() > -1)
            {
                line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line.IsMatch(pattern, options)) yield return line;
            }
        }
        #endregion
    }
}

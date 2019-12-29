using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mendz.Library.Extensions.IO
{
    /// <summary>
    /// Provides TextWriter extensions.
    /// </summary>
    public static class TextWriterExtensions
    {
        /// <summary>
        /// Writes a line that matches a regular expression.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="line">The line to write.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        public static void WriteLineMatch(this TextWriter writer, 
            string line, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (line.IsMatch(pattern, options)) writer.WriteLine(line);
        }

        #region Async
        /// <summary>
        /// Asynchronously writes a line that matches a regular expression.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="line">The line to write.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        public static async Task WriteLineMatchAsync(this TextWriter writer, 
            string line, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (line.IsMatch(pattern, options)) await writer.WriteLineAsync(line).ConfigureAwait(false);
        }
        #endregion
    }
}

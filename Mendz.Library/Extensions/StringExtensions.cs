using System.Text.RegularExpressions;

namespace Mendz.Library.Extensions
{
    /// <summary>
    /// Provides string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if a string matches a regular expression pattern.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>True if matched. Otherwise, false.</returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions options = RegexOptions.None) => Regex.IsMatch(input, pattern, options);

        /// <summary>
        /// Replaces string content that matches a regular expression pattern.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <param name="options">The Regex options.</param>
        /// <returns>The replaced string.</returns>
        public static string ReplaceMatch(this string input, string pattern, string replacement, RegexOptions options = RegexOptions.None) => Regex.Replace(input, pattern, replacement, options);
    }
}

using Mendz.Library.Extensions;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// The base class for stamped filenames.
    /// </summary>
    /// <typeparam name="T">The type of stamp value.</typeparam>
    /// <remarks>
    /// The default stamp format is <stamp>[_#], where # is the non-zero counter value.
    /// If stamped as extension, the format is <filename>[.ext].<stamp>[_#].
    /// Else, the format is <filename>_<stamp>[_#][.ext].
    /// </remarks>
    public abstract class StampedFileNameBase<T>
    {
        private string _fileName;
        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string FileName 
        { 
            get => _fileName;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) throw new ArgumentException("The filename cannot be empty.");
                _fileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the stamp value.
        /// </summary>
        public T StampValue { get; set; }

        private char _separator = '_';
        /// <summary>
        /// Gets or sets the separator. Default is '_'.
        /// </summary>
        public char Separator {
            get => _separator;
            set
            {
                if (value.ToString().Length == 0) throw new ArgumentException("The separator cannot be empty.");
                _separator = value;
            }
        }

        /// <summary>
        /// Gets or sets an indicator if the stamp is in the filename or appended as the extension.
        /// </summary>
        public bool StampAsExtension { get; set; } = true;

        private int _counter = 0;
        /// <summary>
        /// Gets or sets a counter, used to prevent duplicates.
        /// </summary>
        public int Counter
        { 
            get => _counter;
            set => _counter = value < 0 ? 0 : value;
        }

        /// <summary>
        /// Creates an instance of a stamped filename.
        /// </summary>
        /// <param name="filename">The filename to stamp.</param>
        /// <param name="stampValue">The stamp value.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <param name="extension">Indicates if to stamp as extension or not. Default is false.</param>
        /// <param name="counter">The seed counter. Default is 0.</param>
        protected StampedFileNameBase(string filename, T stampValue, char separator = '_', bool extension = false, int counter = 0)
        {
            FileName = filename;
            StampValue = stampValue;
            Separator = separator;
            StampAsExtension = extension;
            Counter = counter;
        }

        /// <summary>
        /// Returns the stamped filename, retrying with an incremeted counter until a non-existing filename is generated.
        /// </summary>
        /// <returns>The stamped filename.</returns>
        /// <remarks>
        /// If the stamped filename already exists, the Counter is incremented until a non-existing stamped filename is generated.
        /// Note that only non-zero Counter is appended to the stamped filename.
        /// </remarks>
        public virtual string ToStringWithRetry()
        {
            string stampedFileName;
            while (true)
            {
                stampedFileName = ToString();
                if (File.Exists(stampedFileName)) Counter++;
                else break;
            }
            return stampedFileName;
        }

        /// <summary>
        /// Formats the stamp value.
        /// </summary>
        /// <returns>The formatted stamp value.</returns>
        protected virtual string FormatStampValue() => StampValue.ToString();

        /// <summary>
        /// Returns the stamped filename.
        /// </summary>
        /// <returns>The stamped filename.</returns>
        /// <remarks>
        /// Uses FormatStampValue() to format the stamp value.
        /// If the Counter is not equal to 0, it is appended, separated by the Separator.
        /// </remarks>
        public override string ToString()
        {
            string stamp = FormatStampValue() + (Counter == 0 ? "" : Separator + Counter.ToString());
            string stampedFileName;
            if (StampAsExtension)
            {
                // Append the stamp as filename extension
                stampedFileName = FileName + "." + stamp;
            }
            else
            {
                int extensionDotIndex = FileName.LastIndexOf('.');
                if (extensionDotIndex == -1)
                {
                    // Append the stamp at the end of the extensionless filename
                    stampedFileName = FileName + Separator + stamp;
                }
                else
                {
                    // Insert the stamp before the extension dot
                    stampedFileName = FileName.Insert(extensionDotIndex, Separator + stamp);
                }
            }
            return stampedFileName;
        }

        /// <summary>
        /// Deconstructs the stamped filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="stampValue">The stamp value.</param>
        /// <param name="counter">The counter.</param>
        public void Deconstruct(out string filename, out T stampValue, out int counter)
        {
            filename = FileName;
            stampValue = StampValue;
            counter = Counter;
        }

        /// <summary>
        /// A helper method to help parse a path in to a stamped filename.
        /// </summary>
        /// <param name="path">The path to parse.</param>
        /// <param name="stampPattern">The stamp pattern.</param>
        /// <param name="separator">The separator. Default is '_'.</param>
        /// <returns>A tuple of the extension indicator, the original unstamped filename and the array of stamp values.</returns>
        /// <exception cref="ArgumentException">
        /// No stamp found on the path provided.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// No stamp found in the filename.
        /// </exception>
        protected static (bool extension, string filename, string[] stamp) ParseHelper(string path, string stampPattern, char separator = '_')
        {
            if (!path.IsMatch(stampPattern)) throw new ArgumentException("No stamp found.", nameof(path));
            // Parse out the filename, without path information
            string stampedFileName = Path.GetFileName(path);
            // Determine if the stamp is an extension
            Match match = Regex.Match(stampedFileName, @"\." + stampPattern + @"$");
            bool extension = match.Success;
            // Parse out the original unstamped filename
            string filename;
            if (extension)
            {
                filename = stampedFileName.ReplaceMatch(@"\." + stampPattern + @"$", "");
            }
            else
            {
                match = Regex.Match(stampedFileName, separator + stampPattern, RegexOptions.RightToLeft);
                if (!match.Success) throw new InvalidOperationException("No stamp found.");
                filename = stampedFileName.ReplaceMatch(separator + stampPattern, "", RegexOptions.RightToLeft);
            }
            // Parse out the stamp
            string[] stamp = match.Value.Substring(1).Split(separator);
            // Put them all together...
            return (extension, filename, stamp);
        }
    }
}

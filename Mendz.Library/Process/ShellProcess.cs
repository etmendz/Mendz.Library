using System;
using System.Diagnostics;

namespace Mendz.Library
{
    /// <summary>
    /// Provides methods to start a shell process (usually, a windowed process).
    /// </summary>
    public static class ShellProcess
    {
        /// <summary>
        /// Starts a shell process given the ProcessStartInfo,
        /// an optional exited event handler and 
        /// an indicator if exceptions are suppressed.
        /// If possible, this method waits for the launched shell process to exit.
        /// </summary>
        /// <param name="startInfo">The ProcessStartInfo instance.</param>
        /// <param name="exitedHandler">The exited event handler.</param>
        /// <param name="suppressException">The indicator to suppress exceptions.</param>
        /// <remarks>
        /// Redirection of standard input, output and error are always set to false.
        /// If ErrorDialog is true, the ErrorDialogParentHandle is required.
        /// If not set, an InvalidOperationException is thrown. This is not affected by the exception suppression setting.
        /// Exception suppression affects only the exceptions thrown by the process component/instance.
        /// </remarks>
        public static void Start(ProcessStartInfo startInfo, 
            EventHandler exitedHandler = null, 
            bool suppressException = false)
        {
            if (startInfo == null) throw new ArgumentNullException(nameof(startInfo));
            startInfo.UseShellExecute = true;
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            if (startInfo.ErrorDialog == true)
            {
                if (startInfo.ErrorDialogParentHandle == IntPtr.Zero)
                {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    throw new InvalidOperationException("Parent handle required.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                }
            }
            using (Process process = new Process())
            {
                try
                {
                    process.StartInfo = startInfo;
                    process.EnableRaisingEvents = true;
                    if (exitedHandler != null) process.Exited += exitedHandler;
                    if (process.Start())
                    {
                        process.WaitForExit();
                        while (!process.HasExited) { }
                        process.CloseMainWindow();
                    }
                }
                catch
                {
                    if (!suppressException) throw;
                }
                finally
                {
                    try
                    {
                        process.CloseMainWindow();
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch
#pragma warning restore CA1031 // Do not catch general exception types
                    {
                        // Shhh...
                    }
                }
            }
        }

        /// <summary>
        /// Starts a command process given the filename,
        /// optional arguments,
        /// an optional exited event handler and 
        /// an indicator if exceptions are suppressed.
        /// If possible, this method waits for the launched command process to exit.
        /// </summary>
        /// <param name="fileName">The command or program path/name.</param>
        /// <param name="arguments">The command or program arguments/parameters.</param>
        /// <param name="exitedHandler">The exited event handler.</param>
        /// <param name="suppressException">The indicator to suppress exceptions.</param>
        public static void Start(string fileName, string arguments = "",
            EventHandler exitedHandler = null,
            bool suppressException = false) => Start(new ProcessStartInfo() { FileName = fileName, Arguments = arguments },
                exitedHandler,
                suppressException);
    }
}

using System;
using System.Diagnostics;

namespace Mendz.Library
{
    /// <summary>
    /// Provides methods to start a command process (usually, a window-less process).
    /// </summary>
    public static class CommandProcess
    {
        /// <summary>
        /// Starts a command process given the ProcessStartInfo,
        /// optional event handlers, 
        /// an indicator if exceptions are suppressed and 
        /// a timeout setting.
        /// If possible, this method waits for the launched command process to exit.
        /// </summary>
        /// <param name="startInfo">The ProcessStartInfo instance.</param>
        /// <param name="outputHandler">The output event handler.</param>
        /// <param name="errorHandler">The error event handler.</param>
        /// <param name="exitedHandler">The exited event handler.</param>
        /// <param name="suppressException">The indicator to suppress exceptions.</param>
        /// <param name="timeout">The timeout setting in milliseconds. Default is -1. If 0 or less, defaults to -1.</param>
        /// <returns>The exit code.</returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="startInfo"/> parameter is required.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the ProcessStartInfo.RedirectStandardOutput is true, the output handler is required.
        /// If the ProcessStartInfo.RedirectStandardError is true, the error handler is required.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Redirection of standard input is always set to false.
        /// If RedirectStandardOutput is true, the outputHandler is required.
        /// If not set, an InvalidOperationException is thrown. This is not affected by the exception suppression setting.
        /// If RedirectStandardError is true, the errorHandler is required.
        /// If not set, an InvalidOperationException is thrown. This is not affected by the exception suppression setting.
        /// Exception suppression affects only the exceptions thrown by the process component/instance.
        /// </para>
        /// <para>
        /// If timeout is greater than 0 and the process WaitForExit(timeout) returns false,
        /// the process Kill() is called. Thus, setting the timeout can be used to terminate
        /// processes that are assumed hanging after running beyond the timeout setting.
        /// </para>
        /// </remarks>
        public static int Start(ProcessStartInfo startInfo,
            DataReceivedEventHandler outputHandler = null,
            DataReceivedEventHandler errorHandler = null,
            EventHandler exitedHandler = null,
            bool suppressException = false,
            int timeout = -1)
        {
            if (startInfo == null) throw new ArgumentNullException(nameof(startInfo));
            int exitCode = 0;
            startInfo.UseShellExecute = false;
            startInfo.ErrorDialog = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardInput = false;
            if (startInfo.RedirectStandardOutput)
            {
                if (outputHandler == null) throw new InvalidOperationException("Output handler is required.");
            }
            if (startInfo.RedirectStandardError)
            {
                if (errorHandler == null) throw new InvalidOperationException("Error handler is required.");
            }
            timeout = timeout <= 0 ? -1 : timeout;
            using (Process process = new Process())
            {
                try
                {
                    process.StartInfo = startInfo;
                    process.EnableRaisingEvents = true;
                    if (outputHandler != null)
                    {
                        startInfo.RedirectStandardOutput = true;
                        process.OutputDataReceived += outputHandler;
                    }
                    if (errorHandler != null)
                    {
                        startInfo.RedirectStandardError = true;
                        process.ErrorDataReceived += errorHandler;
                    }
                    if (exitedHandler != null) process.Exited += exitedHandler;
                    if (process.Start())
                    {
                        if (startInfo.RedirectStandardOutput) process.BeginOutputReadLine();
                        if (startInfo.RedirectStandardError) process.BeginErrorReadLine();
                        if (!process.WaitForExit(timeout))
                        {
                            process.Kill();
                            process.WaitForExit();
                        }
                        while (!process.HasExited) { }
                        exitCode = process.ExitCode;
                    }
                }
                catch
                {
                    if (!suppressException) throw;
                }
            }
            return exitCode;
        }

        /// <summary>
        /// Starts a command process given the filename,
        /// optional arguments,
        /// optional event handlers, 
        /// an indicator if exceptions are suppressed and 
        /// a timeout setting.
        /// If possible, this method waits for the launched command process to exit.
        /// </summary>
        /// <param name="fileName">The command or program path/name.</param>
        /// <param name="arguments">The command or program arguments/parameters.</param>
        /// <param name="outputHandler">The output event handler.</param>
        /// <param name="errorHandler">The error event handler.</param>
        /// <param name="exitedHandler">The exited event handler.</param>
        /// <param name="suppressException">The indicator to suppress exceptions.</param>
        /// <param name="timeout">The timeout setting in milliseconds. Default is -1. If 0 or less, defaults to -1.</param>
        /// <returns>The exit code.</returns>
        public static int Start(string fileName, string arguments = "",
            DataReceivedEventHandler outputHandler = null,
            DataReceivedEventHandler errorHandler = null,
            EventHandler exitedHandler = null,
            bool suppressException = false,
            int timeout = -1) => Start(new ProcessStartInfo() { FileName = fileName, Arguments = arguments }, 
                outputHandler, 
                errorHandler, 
                exitedHandler, 
                suppressException, 
                timeout);
    }
}

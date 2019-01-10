using System.Runtime.InteropServices;

namespace HostedApp.Helpers
{
    internal class OS
    {
        /// <summary>
        /// Determines whether this instance is windows.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is windows; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// Determines whether this instance is linux.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is linux; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        /// Determines whether this instance is mac os.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is mac os; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsMacOS() => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}

using System;
using System.Reflection;

namespace HostedApp.Helpers
{
    internal static class DotNetCore
    {
        /// <summary>
        /// Gets the current version.
        /// </summary>
        /// <returns></returns>
        internal static string GetCurrentVersion()
        {
            var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
            var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
            if (netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2)
                return assemblyPath[netCoreAppIndex + 1];
            return null;
        }
    }
}

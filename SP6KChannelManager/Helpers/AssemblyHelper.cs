using System.Reflection;

namespace SP6KChannelManager.Helpers
{
    /// <summary>
    /// Provides access to metadata from the executing assembly.
    /// </summary>
    internal static class AssemblyHelper
    {
        /// <summary>
        /// Gets the copyright information defined for the assembly.
        /// </summary>
        internal static string Copyright => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? "-";

        /// <summary>
        /// Gets the informational version defined for the assembly.
        /// </summary>
        internal static string InformationalVersion => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "-";

        /// <summary>
        /// Gets the product name defined for the assembly.
        /// </summary>
        internal static string Product => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "-";

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        internal static Version Version => Assembly.GetExecutingAssembly().GetName().Version ?? new();

        /// <summary>
        /// Gets the description defined for the assembly.
        /// </summary>
        internal static string Description => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "-";

        /// <summary>
        /// Gets the company name defined for the assembly.
        /// </summary>
        internal static string Company => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "-";
    }
}

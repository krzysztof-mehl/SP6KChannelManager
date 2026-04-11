using SP6KChannelManager.Helpers;
using System.IO;
using System.Reflection;

namespace SP6KChannelManager.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public static string ProductName => AssemblyHelper.Product;

        public static string Version => AssemblyHelper.Version.ToString();

        public static string InformationalVersion => AssemblyHelper.InformationalVersion;

        public static string Company => AssemblyHelper.Company;

        public static string Copyright => AssemblyHelper.Copyright;

        public static string Description => AssemblyHelper.Description;

        public string LicenseText { get; set; } = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SP6KChannelManager.LICENSE")!).ReadToEnd();
    }
}

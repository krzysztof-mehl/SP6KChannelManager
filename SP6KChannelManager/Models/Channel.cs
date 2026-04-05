using SP6KChannelManager.ViewModels;

namespace SP6KChannelManager.Models
{
    public class Channel : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";
        public string Callsign { get; set => SetProperty(ref field, value); } = "";
    }
}

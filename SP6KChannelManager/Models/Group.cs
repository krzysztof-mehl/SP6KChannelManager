using SP6KChannelManager.ViewModels;
using System.Collections.ObjectModel;

namespace SP6KChannelManager.Models
{
    public class Group : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";
        public ObservableCollection<Channel> Channels { get; set => SetProperty(ref field, value); } = [];
        public Channel? SelectedChannel { get; set { IsChannelSelected = value != null; SetProperty(ref field, value); } } = null;
        public bool IsChannelSelected { get; private set => SetProperty(ref field, value); } = false;
    }
}

using SP6KChannelManager.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;

namespace SP6KChannelManager.Models
{
    public class Channel : BaseViewModel
    {
        public string Name
        {
            get;
            set => SetProperty(ref field, value);
        }
            = "";

        public Channel Copy()
        {
            return new Channel { Name = this.Name };
        }

        public void CopyFrom(Channel source)
        {
            Name = source.Name;
        }
    }

    public class Group : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";

        public Channel? SelectedChannel
        {
            get;
            set
            {
                if (SetProperty(ref field, value))
                {
                    if (value != null)
                    {
                        EditedChannel = value?.Copy();
                    }
                    else
                    {
                        EditedChannel = null;
                    }
                }
            }
        } = null;

        public Channel? EditedChannel { get; set => SetProperty(ref field, value); } = null;

        public ObservableCollection<Channel> Channels { get; set; } = [];
    }

    public class GroupCollection : BaseViewModel
    {
        public Group? SelectedGroup { get; set => SetProperty(ref field, value); } = null;

        public int ChannelsCount
        {
            get
            {
                return Groups.Sum(group => group.Channels.Count);
            }
        }

        public ObservableCollection<Group> Groups { get; set; } = [];
    }
}

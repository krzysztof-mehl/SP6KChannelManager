using SP6KChannelManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP6KChannelManager.Models
{
    public class Channel : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";

        public Channel()
        {
        }

        public Channel(Channel? source)
        {
            if (source != null)
            {
                Name = source.Name;
            }
        }
    }
}

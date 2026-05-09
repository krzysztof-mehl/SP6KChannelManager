using SP6KChannelManager.Helpers;
using SP6KChannelManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SP6KChannelManager.Models
{
    public class Project : BaseViewModel
    {
        public Version Version { get; private set; } = AssemblyHelper.Version;
        public string FilePath { get; private set; } = "";
        public bool RequireProjectSaveConfirmation { get; private set; } = true;

        // Group settings
        public string GroupNamePattern { get; private set; } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string GroupNamePatternDescription { get; private set; } = $"Group name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public bool RequireUniqueGroupNames { get; private set; } = true;

        // Channel settings
        public string ChannelNamePattern { get; private set; } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string ChannelNamePatternDescription { get; private set; } = $"Channel name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public bool RequireUniqueChannelNames { get; private set; } = true;
        public List<string> ChannelStatuses { get; private set; } = ["Inactive ", "Unverified", "Verifying", "Active ", "Verified "];
        public string CallsignPattern { get; private set; } = @"^([A-Z0-9]{1,3}[0-9][A-Z0-9]{0,3}[A-Z])$";
        public string CallsignPatternDescription { get; private set; } = "Callsign must follow the ITU amateur callsign format.";
        public bool RequireUniqueCallsignsInBand { get; private set; } = true;
        public List<decimal> CtcssTones { get; private set; } = [67.0m, 69.3m, 71.9m, 74.4m, 77.0m, 79.7m, 82.5m, 85.4m, 88.5m, 91.5m, 94.8m, 97.4m, 100.0m, 103.5m, 107.2m, 110.9m, 114.8m, 118.8m, 123.0m, 127.3m, 131.8m, 136.5m, 141.3m, 146.2m, 151.4m, 156.7m, 159.8m, 162.2m, 165.5m, 167.9m, 171.3m, 173.8m, 177.3m, 179.9m, 183.5m, 186.2m, 189.9m, 192.8m, 196.6m, 199.5m, 203.5m, 206.5m, 210.7m, 218.1m, 225.7m, 229.1m, 233.6m, 241.8m, 250.3m, 254.1m];
    
    
        public ObservableCollection<Group> Groups { get; set { SetProperty(ref field, value); } } = [];
    }
}

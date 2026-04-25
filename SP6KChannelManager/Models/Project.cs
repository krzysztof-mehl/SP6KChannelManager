using SP6KChannelManager.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP6KChannelManager.Models
{
    class Project
    {
        public Version Version { get; set; } = AssemblyHelper.Version;
        public string FilePath { get; set; } = "";
        public bool RequireProjectSaveConfirmation { get; set; } = true;

        // Group settings
        public string GroupNamePattern { get; set; } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string GroupNamePatternDescription { get; set; } = $"Group name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public bool RequireUniqueGroupNames { get; set; } = true;

        // Channel settings
        public string ChannelNamePattern { get; set; } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string ChannelNamePatternDescription { get; set; } = $"Channel name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public bool RequireUniqueChannelNames { get; set; } = true;
        public List<string> ChannelStatuses { get; set; } = ["Disabled", "Inactive", "Under Test", "Active"];
        public string CallsignPattern { get; set; } = @"^([A-Z0-9]{1,3}[0-9][A-Z0-9]{0,3}[A-Z])$";
        public string CallsignPatternDescription { get; set; } = "Callsign must follow the ITU amateur callsign format.";
        public bool RequireUniqueCallsignsInBand { get; set; } = true;
        public List<decimal> CtcssTones { get; set; } = [67.0m, 69.3m, 71.9m, 74.4m, 77.0m, 79.7m, 82.5m, 85.4m, 88.5m, 91.5m, 94.8m, 97.4m, 100.0m, 103.5m, 107.2m, 110.9m, 114.8m, 118.8m, 123.0m, 127.3m, 131.8m, 136.5m, 141.3m, 146.2m, 151.4m, 156.7m, 159.8m, 162.2m, 165.5m, 167.9m, 171.3m, 173.8m, 177.3m, 179.9m, 183.5m, 186.2m, 189.9m, 192.8m, 196.6m, 199.5m, 203.5m, 206.5m, 210.7m, 218.1m, 225.7m, 229.1m, 233.6m, 241.8m, 250.3m, 254.1m];

    }
}

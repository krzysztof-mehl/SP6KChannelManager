using SP6KChannelManager.Helpers;
using SP6KChannelManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP6KChannelManager.Models
{
    public class Project : BaseViewModel
    {
        public Version Version { get; set; } = AssemblyHelper.Version;

        public int WindowHeight { get; set => SetProperty(ref field, value); } = 600;
        public int WindowWidth { get; set => SetProperty(ref field, value); } = 1100;

        public bool UniqueGroupNames { get; set; } = true;
        public bool UniqueChannelNames { get; set; } = false;
        public bool UniqueChannelNamesInGroup { get; set; } = true;

        public bool ShowDebugInfo { get; set; } = false;

        public ErrorHandler ErrorHandler { get; set; } = new();

        public GroupCollection GroupCollection { get; set; } = new();
    }
}

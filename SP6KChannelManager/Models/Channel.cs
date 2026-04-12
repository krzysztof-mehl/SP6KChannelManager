using SP6KChannelManager.ViewModels;
using System.Text.RegularExpressions;

namespace SP6KChannelManager.Models
{
    public class Channel : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";
        public string Callsign { get; set { if (Name == "") Name = value; SetProperty(ref field, value); } } = "";
        public decimal? Frequency { get; set => SetProperty(ref field, value); } = null;
        public decimal? Offset { get; set => SetProperty(ref field, value); } = null;
        public string Comment { get; set => SetProperty(ref field, value); } = "";

        public bool UseLocation { get; set => SetProperty(ref field, value); } = false;
        public string Qth { get; set => SetProperty(ref field, value); } = "";
        public string Locator { get; set => SetProperty(ref field, value); } = "";
        public decimal? Latitude { get; set => SetProperty(ref field, value); } = null;
        public decimal? Longitude { get; set => SetProperty(ref field, value); } = null;

        public bool UseModeFm { get; set => SetProperty(ref field, value); } = false;
        public string Bandwidth { get; set => SetProperty(ref field, value); } = "";
        public string Tone { get; set => SetProperty(ref field, value); } = "";
        public decimal? CtcssTone { get; set => SetProperty(ref field, value); } = null;

        public bool UseModeC4Fm { get; set => SetProperty(ref field, value); } = false;

        public bool UseModeDmr { get; set => SetProperty(ref field, value); } = false;
        public string Timeslot { get; set => SetProperty(ref field, value); } = "";
        public int? Cc { get; set => SetProperty(ref field, value); } = null;

        public bool UseModeDv { get; set => SetProperty(ref field, value); } = false;
        public string Ur { get; set => SetProperty(ref field, value); } = "";
        public string Rpt1 { get; set => SetProperty(ref field, value); } = "";
        public string Rpt2 { get; set => SetProperty(ref field, value); } = "";

        public bool UseModeAm { get; set => SetProperty(ref field, value); } = false;

        public Channel()
        {
        }

        public Channel(Channel? source)
        {
            if (source != null)
            {
                Name = source.Name;
                Callsign = source.Callsign;
                Frequency = source.Frequency;
                Offset = source.Offset;
                Comment = source.Comment;
                UseLocation = source.UseLocation;
                Qth = source.Qth;
                Locator = source.Locator;
                Latitude = source.Latitude;
                Longitude = source.Longitude;
                UseModeFm = source.UseModeFm;
                Bandwidth = source.Bandwidth;
                Tone = source.Tone;
                CtcssTone = source.CtcssTone;
                UseModeC4Fm = source.UseModeC4Fm;
                UseModeDmr = source.UseModeDmr;
                Timeslot = source.Timeslot;
                Cc = source.Cc;
                UseModeDv = source.UseModeDv;
                Ur = source.Ur;
                Rpt1 = source.Rpt1;
                Rpt2 = source.Rpt2;
                UseModeAm = source.UseModeAm;
            }
        }

        public bool Validate(ErrorHandler errorHandler, Project project, Group selectedGroup)
        {
            if (!ValidateName(errorHandler, project, selectedGroup)) { return false; }
            return true;
        }

        public static bool ValidateName(ErrorHandler errorHandler, Project project, string name, Group selectedGroup)
        {
            if (!Regex.IsMatch(name, project.ChannelNamePattern))
            {
                ErrorHandler.AddError(errorHandler, $"{project.ChannelNamePatternDescription}\n\n{project.ChannelNamePattern}");
                return false;
            }
            if (selectedGroup.SelectedChannel != null && selectedGroup.SelectedChannel.Name == name)
            {
                return true;
            }
            if (selectedGroup.Channels.Any(c => c.Name == name))
            {
                ErrorHandler.AddError(errorHandler, "Channel name must be unique within the group.");
                return false;
            }
            return true;
        }

        public bool ValidateName(ErrorHandler errorHandler, Project project, Group selectedGroup)
        {
            return ValidateName(errorHandler, project, Name, selectedGroup);
        }

        public bool ValidateCallsign(ErrorHandler errorHandler, Project project)
        {
            if (Callsign == "") return true;
            if (!Regex.IsMatch(Callsign, project.CallsignPattern))
            {
                ErrorHandler.AddError(errorHandler, $"{project.CallsignPatternDescription}\n\n{project.CallsignPattern}");
                return false;
            }
            return true;
        }
    }
}

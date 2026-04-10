using SP6KChannelManager.ViewModels;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SP6KChannelManager.Models
{
    public class Channel : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";
        public string Callsign { get; set => SetProperty(ref field, value); } = "";
        public decimal? Frequency { get; set => SetProperty(ref field, value); } = null;
        [JsonIgnore] public string FrequencyString { get; set { SetProperty(ref field, value); } } = "";
        public decimal? Offset { get; set => SetProperty(ref field, value); } = null;
        [JsonIgnore] public string OffsetString { get; set { SetProperty(ref field, value); } } = "";
        public string Comment { get; set => SetProperty(ref field, value); } = "";

        public bool UseLocation { get; set => SetProperty(ref field, value); } = false;
        public string Qth { get; set => SetProperty(ref field, value); } = "";
        public string Locator { get; set => SetProperty(ref field, value); } = "";
        public decimal? Latitude { get; set => SetProperty(ref field, value); } = null;
        public decimal? Longitude { get; set => SetProperty(ref field, value); } = null;

        public bool UseModeAm { get; set => SetProperty(ref field, value); } = false;

        public bool UseModeC4Fm { get; set => SetProperty(ref field, value); } = false;

        public bool UseModeDmr { get; set => SetProperty(ref field, value); } = false;
        public string Timeslot { get; set => SetProperty(ref field, value); } = "";
        public int? Cc { get; set => SetProperty(ref field, value); } = null;

        public bool UseModeDv { get; set => SetProperty(ref field, value); } = false;
        public string Ur { get; set => SetProperty(ref field, value); } = "";
        public string Rpt1 { get; set => SetProperty(ref field, value); } = "";
        public string Rpt2 { get; set => SetProperty(ref field, value); } = "";

        public bool UseModeFm { get; set => SetProperty(ref field, value); } = false;
        public string Bandwidth { get; set => SetProperty(ref field, value); } = "";
        public string Tone { get; set => SetProperty(ref field, value); } = "";
        public decimal? CtcssTone { get; set => SetProperty(ref field, value); } = null;

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
                FrequencyString = Frequency.ToString() ?? "";
                Offset = source.Offset;
                OffsetString = Offset.ToString() ?? "";
                Comment = source.Comment;
                UseLocation = source.UseLocation;
                Qth = source.Qth;
                Locator = source.Locator;
                Latitude = source.Latitude;
                Longitude = source.Longitude;
                UseModeAm = source.UseModeAm;
                UseModeC4Fm = source.UseModeC4Fm;
                UseModeDmr = source.UseModeDmr;
                Timeslot = source.Timeslot;
                Cc = source.Cc;
                UseModeDv = source.UseModeDv;
                Ur = source.Ur;
                Rpt1 = source.Rpt1;
                Rpt2 = source.Rpt2;
                UseModeFm = source.UseModeFm;
                Bandwidth = source.Bandwidth;
                Tone = source.Tone;
                CtcssTone = source.CtcssTone;
            }
        }

        public bool Validate(ErrorHandler errorHandler, Project project, Group selectedGroup)
        {
            if (!ValidateName(errorHandler, project, selectedGroup)) { return false; }
            if (!ValidateCallsign(errorHandler, project)) { return false; }
            if (!ValidateFrequency(errorHandler, project)) { return false; }
            if (!ValidateOffset(errorHandler, project)) { return false; }
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

        public bool ValidateFrequency(ErrorHandler errorHandler, Project project)
        {
            if (FrequencyString == "")
            {
                Frequency = null;
            }
            else
            {
                try
                {
                    FrequencyString = FrequencyString.Trim();
                    Frequency = decimal.Parse(FrequencyString.Replace(',', '.'), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
                }
                catch
                {
                    ErrorHandler.AddError(errorHandler, "Frequency must be a number.");
                    return false;
                }
            }
            if (Frequency == null)
            {
                ErrorHandler.AddError(errorHandler, "Frequency is required.");
                return false;
            }
            if (Frequency < project.FrequencyMin || Frequency > project.FrequencyMax)
            {
                ErrorHandler.AddError(errorHandler, $"Frequency must be between {project.FrequencyMin} and {project.FrequencyMax} MHz.");
                return false;
            }
            return true;
        }

        public bool ValidateOffset(ErrorHandler errorHandler, Project project)
        {
            if (OffsetString == "")
            {
                Offset = null;
            }
            else
            {
                try
                {
                    OffsetString = OffsetString.Trim();
                    Offset = decimal.Parse(OffsetString.Replace(',', '.'), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
                }
                catch
                {
                    ErrorHandler.AddError(errorHandler, "Offset must be a number.");
                    return false;
                }
            }
            if (Offset == null)
            {
                ErrorHandler.AddError(errorHandler, "Offset is required.");
                return false;
            }
            if ((Frequency + Offset) < project.FrequencyMin || (Frequency + Offset) > project.FrequencyMax)
            {
                ErrorHandler.AddError(errorHandler, $"Frequency + Offset must be between {project.FrequencyMin} and {project.FrequencyMax} MHz.");
                return false;
            }
            return true;
        }
    }
}

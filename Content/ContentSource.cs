using Avalonia.Utilities;
using Material.Icons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Vidiya.Managers;

namespace Vidiya.Content
{
    public abstract class ContentSource
    {
        public string id = Guid.NewGuid().ToString();
        public LogManager logger = VidiyaManager.instance.LogManager;

        public string? stateMessage;
        public MaterialIconKind stateIcon = MaterialIconKind.HelpCircleOutline;
        public ContentState state = ContentState.Unknown;
        public event EventHandler<ContentSource>? StateChanged;
        public ObservableCollection<ContentResource> content = new();

        public abstract UserControl GetUserControl();

        public abstract ContentType GetContentType();

        public ContentSourceState GetSaveState()
        {
            DataManager dataManager = VidiyaManager.instance.DataManager;
            string folder = dataManager.GetContentSourcePath(this.id);
            return new ContentSourceState(new Uri(folder), this.GetContentType(), this.state, this.content.ToList());
        }

        public void TriggerRebuild(ContentSourceState contentSourceState)
        {
            contentSourceState.content.ForEach(this.content.Add);
            this.state = contentSourceState.state;
            this.StateChanged?.Invoke(this, this);

            OnRebuild();
        }

        public abstract void OnRebuild();

        public abstract void OnSetup();

        public void Save()
        {
            VidiyaManager.instance.DataManager.SaveContentSource(this);
        }

        public void SetState(ContentState state, MaterialIconKind icon, string? message = null)
        {
            this.state = state;
            this.stateIcon = icon;
            this.stateMessage = message;
            StateChanged?.Invoke(this, this);
            Save();
        }

        public void SetStatePending(string? message = null)
        {
            this.state = ContentState.Pending;
            this.stateIcon = MaterialIconKind.TimerSettingsOutline;
            this.stateMessage = null;
            StateChanged?.Invoke(this, this);
            Save();
        }

        private static MaterialIconKind[] DownloadIcons = new MaterialIconKind[] {
            MaterialIconKind.CircleOutline,
            MaterialIconKind.CircleSlice1,
            MaterialIconKind.CircleSlice2,
            MaterialIconKind.CircleSlice3,
            MaterialIconKind.CircleSlice4,
            MaterialIconKind.CircleSlice5,
            MaterialIconKind.CircleSlice6,
            MaterialIconKind.CircleSlice7,
            MaterialIconKind.CircleSlice8,
        };

        public void SetStateDownloading(double progress = 0, string? message = null)
        {
            this.state = ContentState.Downloading;
            this.stateIcon = DownloadIcons[(int) (progress * (DownloadIcons.Length - 1))];
            this.stateMessage = "Download Progress: " + progress.ToString("P2", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
            if(message != null ) this.stateMessage += " | " + message;
            StateChanged?.Invoke(this, this);
            if(progress == 0) Save();
        }

        public void SetStateReady(string? message = null)
        {
            this.state = ContentState.Ready;
            this.stateIcon = MaterialIconKind.CheckCircleOutline;
            this.stateMessage = null;
            StateChanged?.Invoke(this, this);
            Save();
        }

        public void SetStateError(string? message = null)
        { 
            this.state = ContentState.Error;
            this.stateIcon = MaterialIconKind.AlertCircleOutline;
            this.stateMessage = message;
            StateChanged?.Invoke(this, this);
            Save();
        }

        public void SetStateUnknown(string? message = null)
        {
            this.state = ContentState.Unknown;
            this.stateIcon = MaterialIconKind.HelpCircleOutline;
            this.stateMessage = message;
            StateChanged?.Invoke(this, this);
            Save();
        }
    }

    public class ContentSourceState
    {
        public Uri uri { get; }
        public ContentType type { get; }
        public ContentState state { get; }
        public List<ContentResource> content { get; }

        public ContentSourceState() { }

        public ContentSourceState(Uri uri, ContentType type, ContentState state, List<ContentResource> content)
        {
            this.uri = uri;
            this.type = type;
            this.state = state;
            this.content = content;
        }
    }
    public enum ContentType
    {
        YouTubeSingle = 0,
        YouTubePlaylist = 1,
        SpotifySingle = 2,
        SpotifyPlaylist = 3,
        Folder = 4,
    }
}

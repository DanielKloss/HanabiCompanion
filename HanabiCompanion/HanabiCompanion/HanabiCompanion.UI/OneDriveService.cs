using Microsoft.OneDrive.Sdk;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using HanabiCompanion.UI.Models;

namespace HanabiCompanion.UI
{
    public class OneDriveService : TrackPropertyChanged
    {
        OneDriveClient client;

        private bool _isWorking;
        public bool isWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                OnPropertyChanged(nameof(isWorking));
            }
        }

        private bool _restoring;
        public bool restoring
        {
            get { return _restoring; }
            set
            {
                _restoring = value;
                OnPropertyChanged(nameof(restoring));
            }
        }

        private bool _backingUp;
        public bool backingUp
        {
            get { return _backingUp; }
            set
            {
                _backingUp = value;
                OnPropertyChanged(nameof(backingUp));
            }
        }

        public async Task Authenticate()
        {
            var msaAuthenticationProvider = new OnlineIdAuthenticationProvider(new string[] { "onedrive.readwrite" });
            await msaAuthenticationProvider.AuthenticateUserAsync();
            client = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthenticationProvider);
        }

        public async Task BackUp()
        {
            await Authenticate();

            StorageFile file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("Hanabi.sql") as StorageFile;

            using (var stream = await file.OpenStreamForReadAsync())
            {
                var result = await client.Drive.Root.ItemWithPath("/BoardGameCompanions/Hanabi").Content.Request().PutAsync<Item>(stream);
            }
        }

        public async Task Restore()
        {
            await Authenticate();

            var stream = await client.Drive.Root.ItemWithPath("/BoardGameCompanions/Hanabi").Content.Request().GetAsync();

            using (stream)
            {
                var destination = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync("Hanabi.sql", CreationCollisionOption.ReplaceExisting);
                stream.CopyTo(destination);
            }
        }
    }
}

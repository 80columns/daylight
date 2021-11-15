using Daylight.settings;
using System;
using System.Threading.Tasks;

namespace Daylight.helper {
    public enum UserLocationSource {
        Internet, // default, find location based on IP address
        Custom, // use a custom location the user inputs, retrieve coordinates from google maps api
        None // don't use location, custom times will be used for sunrise & sunset
    }

    public enum AppLogLevel {
        Detailed,
        None
    }

    public class Helper : IDisposable {
        private bool p_Disposed;

        public UserSettings User_Settings;

        public HttpHelper Http;
        public LocationHelper Location;
        public LogHelper Log;

        public Helper() {
            this.Http = new HttpHelper(this);
            this.Location = new LocationHelper(this);
            this.Log = new LogHelper(this);

            this.p_Disposed = false;
        }

        public async Task CreateUserSettingsAsync() {
            this.User_Settings = await UserSettings.CreateAsync().ConfigureAwait(false);
        }

        // https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(
            bool _Disposing
        ) {
            if (
                this.p_Disposed == false
             && _Disposing
            ) {
                this.Http?.Dispose();
                this.Location?.Dispose();
                this.Log?.Dispose();

                this.User_Settings = null;
                this.p_Disposed = true;
            }
        }
    }
}
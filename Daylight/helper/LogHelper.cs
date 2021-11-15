using Daylight.settings;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Daylight.helper {
    public class LogHelper : IDisposable {
        private bool p_Disposed;
        private Helper p_Helper;

        public LogHelper(
            Helper _Helper
        ) {
            this.p_Helper = _Helper;
        }

        public async Task LogAppAsync(
            string _Message,
            [CallerMemberName] string _Caller = ""
        ) {
            if (this.p_Helper.User_Settings.LogLevel == AppLogLevel.Detailed) {
                _Message ??= string.Empty;

                await File.AppendAllTextAsync(
                    path: AppSettings.File.Log.App,
                    contents: $"{DateTimeOffset.Now} >> {_Caller} >> {_Message}{Environment.NewLine}"
                ).ConfigureAwait(false);
            }
        }

        public async Task LogErrorAsync(
            string _Message
        ) {
            if (this.p_Helper.User_Settings.LogLevel == AppLogLevel.Detailed) {
                _Message ??= string.Empty;

                await File.AppendAllTextAsync(
                    path: AppSettings.File.Log.Error,
                    contents: $"{DateTimeOffset.Now} >> {_Message}{Environment.NewLine}"
                ).ConfigureAwait(false);
            }
        }

        public async Task LogExceptionAsync(
            Exception _Exception
        ) {
            if (this.p_Helper.User_Settings.LogLevel == AppLogLevel.Detailed) {
                var Message = $"Exception thrown: Message = '{_Exception?.Message}', Stack trace = '{_Exception?.StackTrace}', Source = '{_Exception?.Source}', Class = '{_Exception?.TargetSite?.DeclaringType?.Name}', Method = '{_Exception?.TargetSite?.Name}'";

                await this.LogErrorAsync(Message).ConfigureAwait(false);
            }
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
                this.p_Helper = null;
                this.p_Disposed = true;
            }
        }
    }
}
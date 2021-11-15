using Daylight.settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Daylight.helper {
    public class HttpHelper : IDisposable {
        private bool p_Disposed;
        private ServiceProvider p_Service_Provider;
        //private IHttpClientFactory p_Client_Factory;
        private Helper p_Helper;

        public HttpHelper(
            Helper _Helper
        ) {
            this.p_Helper = _Helper;

            var sc = new ServiceCollection();
            sc.AddHttpClient<DaylightClient>();

            // set up http client factory for later use
            this.p_Service_Provider = sc.BuildServiceProvider();
            //this.p_Client_Factory = this.p_Service_Provider.GetService<IHttpClientFactory>();
        }

        public DaylightClient GetClient() {
            return this.p_Service_Provider.GetRequiredService<DaylightClient>();
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
                this.p_Service_Provider.Dispose();
                this.p_Helper = null;
                this.p_Disposed = true;
            }
        }
    }

    public class DaylightClient {
        HttpClient Client { get; }

        public DaylightClient(
            HttpClient _Client
        ) {
            this.Client = _Client;
            this.Client.DefaultRequestHeaders.UserAgent.ParseAdd($"{AppSettings.App.Name}/{AppSettings.App.Version} {Environment.OSVersion.VersionString}");
        }

        public async Task<string> GetAsync(
            string _Uri
        ) {
            using (var Request = new HttpRequestMessage(HttpMethod.Get, _Uri))
            using (var Response = await this.Client.SendAsync(Request)) {
                Response.EnsureSuccessStatusCode();

                var Response_Content = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return Response_Content;
            }
        }
    }
}
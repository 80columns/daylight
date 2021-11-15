using Daylight.exception;
using Daylight.model;
using Daylight.settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Daylight.helper {
    public class LocationHelper : IDisposable {
        private bool p_Disposed;
        private Helper p_Helper;

        public LocationHelper(
            Helper _Helper
        ) {
            this.p_Helper = _Helper;
        }

        public async Task<((string Latitude, string Longitude)? Coordinates, string Location)> GetCoordinatesAndLocationAsync() {
            if (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Internet) {
                return await GetIpCoordinatesAsync().ConfigureAwait(false);
            } else if (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Custom) {
                return (
                    Coordinates: await GetCustomLocationCoordinatesAsync(
                        this.p_Helper.User_Settings.CustomLocation
                    ).ConfigureAwait(false),
                    Location: this.p_Helper.User_Settings.CustomLocation
                );
            } else {
                return (
                    Coordinates: null,
                    Location: string.Empty
                );
            }
        }

        public async Task<((string Latitude, string Longitude)? Coordinates, string Location)> GetIpCoordinatesAsync() {
            try {
                var Client = this.p_Helper.Http.GetClient();
                var Response_Content = await Client.GetAsync(AppSettings.Url.IP_Coordinates).ConfigureAwait(false);

                await this.p_Helper.Log.LogAppAsync($"http response: '{Response_Content}'").ConfigureAwait(false);

                var IP_Location_Details = IpLocation.Create(_JSON: Response_Content);

                if (
                    IP_Location_Details == null
                    || IP_Location_Details.latitude.HasValue == false
                    || IP_Location_Details.longitude.HasValue == false
                ) {
                    throw new IpLocationLookupException("Unable to pull location from IP address");
                }

                return (
                    (
                        Latitude: IP_Location_Details.GetLatitude(),
                        Longitude: IP_Location_Details.GetLongitude()
                    ),
                    Location: $"{IP_Location_Details.city}, {IP_Location_Details.region}, {IP_Location_Details.country}"
                );
            } catch (IpLocationLookupException ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex).ConfigureAwait(false);

                throw;
            } catch (Exception ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex).ConfigureAwait(false);

                throw new IpLocationLookupException(
                    message: "Unable to pull location from IP address",
                    innerException: ex
                );
            }
        }

        public async Task<(string Latitude, string Longitude)?> GetCustomLocationCoordinatesAsync(
            string _Custom_Location
        ) {
            try {
                if (string.IsNullOrWhiteSpace(_Custom_Location)) {
                    throw new GeocodingLookupException($"Unable to pull custom location details for empty location");
                }

                var Client = this.p_Helper.Http.GetClient();
                var Response_Content = await Client.GetAsync($"{AppSettings.Url.Geocoding}?q={HttpUtility.UrlEncode(_Custom_Location)}&format=geocodejson&limit=1").ConfigureAwait(false);

                await this.p_Helper.Log.LogAppAsync($"http response: '{Response_Content}'").ConfigureAwait(false);

                var Geocoding_Details = Geocoding.Create(_JSON: Response_Content);

                if (
                    Geocoding_Details == null
                 || Geocoding_Details.features.Length == 0
                 || Geocoding_Details.features[0].geometry.coordinates.Length != 2
                 || Geocoding_Details.features[0].geometry.coordinates[0].HasValue == false
                 || Geocoding_Details.features[0].geometry.coordinates[1].HasValue == false
                ) {
                    throw new GeocodingLookupException($"Unable to pull custom location details for location '{_Custom_Location}'");
                }

                return (
                    Latitude: Geocoding_Details.GetLatitude(),
                    Longitude: Geocoding_Details.GetLongitude()
                );
            } catch (GeocodingLookupException ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex).ConfigureAwait(false);

                throw;
            } catch (Exception ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex).ConfigureAwait(false);

                throw new GeocodingLookupException(
                    message: $"Unable to pull custom location details for location '{_Custom_Location}'",
                    innerException: ex
                );
            }
        }

        public async Task<(TimeSpan Sunrise, TimeSpan Sunset)> GetSunriseAndSunsetAsync(
            (string Latitude, string Longitude)? _Coordinates
        ) {
            try {
                var Sunrise = null as TimeSpan?;
                var Sunset = null as TimeSpan?;

                // pass internal latitude & longitude to https://sunrise-sunset.org/api, get sunrise & sunset
                if (
                    this.p_Helper.User_Settings.LocationSource != UserLocationSource.None
                 && _Coordinates.HasValue
                 && string.IsNullOrWhiteSpace(_Coordinates.Value.Latitude) == false
                 && string.IsNullOrWhiteSpace(_Coordinates.Value.Longitude) == false
                ) {
                    var Client = this.p_Helper.Http.GetClient();
                    var Response_Content = await Client.GetAsync($"{AppSettings.Url.Sunrise_Sunset}?lat={_Coordinates.Value.Latitude}&lng={_Coordinates.Value.Longitude}&formatted=0").ConfigureAwait(false);

                    await this.p_Helper.Log.LogAppAsync($"http response: '{Response_Content}'").ConfigureAwait(false);

                    var Sunrise_Sunset_Details = SunriseSunset.Create(_JSON: Response_Content);

                    if (
                        Sunrise_Sunset_Details == null
                        || Sunrise_Sunset_Details.results == null
                        || string.IsNullOrWhiteSpace(Sunrise_Sunset_Details.results.sunrise)
                        || string.IsNullOrWhiteSpace(Sunrise_Sunset_Details.results.sunset)
                    ) {
                        throw new SunriseSunsetLookupException($"Unable to retrieve sunrise/sunset times for coordinates ({_Coordinates.Value.Latitude}, {_Coordinates.Value.Longitude})");
                    }

                    Sunrise = Sunrise_Sunset_Details.GetLocalSunriseTime();
                    Sunset = Sunrise_Sunset_Details.GetLocalSunsetTime();
                } else {
                    Sunrise = TimeSpan.Parse(this.p_Helper.User_Settings.CustomSunriseTime);
                    Sunset = TimeSpan.Parse(this.p_Helper.User_Settings.CustomSunsetTime);
                }

                return (
                    Sunrise: Sunrise.Value,
                    Sunset: Sunset.Value
                );
            } catch (SunriseSunsetLookupException ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex).ConfigureAwait(false);

                throw;
            } catch (Exception ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex).ConfigureAwait(false);

                throw new SunriseSunsetLookupException(
                    message: $"Unable to retrieve sunrise/sunset times for coordinates ({(_Coordinates.HasValue ? $"{_Coordinates.Value.Latitude}, {_Coordinates.Value.Longitude}" : "null")})",
                    innerException: ex
                );
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
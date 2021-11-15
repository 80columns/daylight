using System;
using System.Text.Json;

namespace Daylight.model {
    /*
     * Sample object:
     *
     * {
     *     "ip": "193.36.224.114",
     *     "version": "IPv4",
     *     "city": "Miami",
     *     "region": "Florida",
     *     "region_code": "FL",
     *     "country": "US",
     *     "country_name": "United States",
     *     "country_code": "US",
     *     "country_code_iso3": "USA",
     *     "country_capital": "Washington",
     *     "country_tld": ".us",
     *     "continent_code": "NA",
     *     "in_eu": false,
     *     "postal": "33192",
     *     "latitude": 25.7689,
     *     "longitude": -80.1946,
     *     "timezone": "America/New_York",
     *     "utc_offset": "-0500",
     *     "country_calling_code": "+1",
     *     "currency": "USD",
     *     "currency_name": "Dollar",
     *     "languages": "en-US,es-US,haw,fr",
     *     "country_area": 9629091.0,
     *     "country_population": 327167434,
     *     "asn": "AS8100",
     *     "org": "ASN-QUADRANET-GLOBAL"
     * }
     *
     */

    public class IpLocation {
        public string ip { get; set; }
        public string version { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
        public string country_code { get; set; }
        public string country_code_iso3 { get; set; }
        public string country_capital { get; set; }
        public string country_tld { get; set; }
        public string continent_code { get; set; }
        public bool? in_eu { get; set; }
        public string postal { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string timezone { get; set; }
        public string utc_offset { get; set; }
        public string country_calling_code { get; set; }
        public string currency { get; set; }
        public string currency_name { get; set; }
        public string languages { get; set; }
        public decimal? country_area { get; set; }
        public decimal? country_population { get; set; }
        public string asn { get; set; }
        public string org { get; set; }

        public static IpLocation Create(
            string _JSON
        ) {
            return JsonSerializer.Deserialize<IpLocation>(_JSON);
        }

        public string GetLatitude() {
            return Convert.ToString(this.latitude.Value);
        }

        public string GetLongitude() {
            return Convert.ToString(this.longitude.Value);
        }
    }
}
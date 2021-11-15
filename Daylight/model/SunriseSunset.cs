using System;
using System.Text.Json;

namespace Daylight.model {
    /*
     * Sample object:
     * 
     * {
     *     "results": {
     *         "sunrise": "2021-11-15T11:37:45+00:00",
     *         "sunset": "2021-11-15T22:33:08+00:00",
     *         "solar_noon": "2021-11-15T17:05:26+00:00",
     *         "day_length": 39323,
     *         "civil_twilight_begin": "2021-11-15T11:14:44+00:00",
     *         "civil_twilight_end": "2021-11-15T22:56:08+00:00",
     *         "nautical_twilight_begin": "2021-11-15T10:46:56+00:00",
     *         "nautical_twilight_end": "2021-11-15T23:23:56+00:00",
     *         "astronomical_twilight_begin": "2021-11-15T10:19:30+00:00",
     *         "astronomical_twilight_end": "2021-11-15T23:51:23+00:00"
     *     },
     *     "status": "OK"
     * }
     * 
     */

    public class SunriseSunset {
        public SunriseSunsetResults results { get; set; }
        public string status { get; set; }

        public static SunriseSunset Create(
            string _JSON
        ) {
            return JsonSerializer.Deserialize<SunriseSunset>(_JSON);
        }

        public TimeSpan GetLocalSunriseTime() {
            return this.GetLocalTime(this.results.sunrise);
        }

        public TimeSpan GetLocalSunsetTime() {
            return this.GetLocalTime(this.results.sunset);
        }

        private TimeSpan GetLocalTime(
            string _UTC_Timestamp
        ) {
            return DateTimeOffset.Parse(_UTC_Timestamp).ToLocalTime().TimeOfDay;
        }
    }

    public class SunriseSunsetResults {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string solar_noon { get; set; }
        public long? day_length { get; set; }
        public string civil_twilight_begin { get; set; }
        public string civil_twilight_end { get; set; }
        public string nautical_twilight_begin { get; set; }
        public string nautical_twilight_end { get; set; }
        public string astronomical_twilight_begin { get; set; }
        public string astronomical_twilight_end { get; set; }
    }
}
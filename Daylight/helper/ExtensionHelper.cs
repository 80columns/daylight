using System;

namespace Daylight.helper {
    public static class ExtensionHelper {
        public static string ToDisplayString(
            this UserLocationSource _Location_Source
        ) {
            return _Location_Source switch {
                UserLocationSource.Internet => "Auto-Detect",
                UserLocationSource.Custom => "Custom Location",
                UserLocationSource.None => "Custom Times",

                _ => throw new ArgumentException(
                         message: $"Invalid user location source {_Location_Source}",
                         paramName: nameof(_Location_Source)
                     )
            };
        }

        public static DateTime GetDate(
            this TimeSpan _Time
        ) {
            var Now = DateTime.Now;

            return new DateTime(
                year: Now.Year,
                month: Now.Month,
                day: Now.Day,
                hour: _Time.Hours,
                minute: _Time.Minutes,
                second: _Time.Seconds
            );
        }

        public static DateTime GenerateDate(
            int _Hour = 0,
            int _Minute = 0,
            int _Second = 0
        ) {
            var Now = DateTime.Now;

            return new DateTime(
                year: Now.Year,
                month: Now.Month,
                day: Now.Day,
                hour: _Hour,
                minute: _Minute,
                second: _Second
            );
        }
    }
}
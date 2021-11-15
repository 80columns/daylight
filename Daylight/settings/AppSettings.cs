using Daylight.helper;
using System;
using System.Drawing;

namespace Daylight.settings {
    public static class AppSettings {
        public static class App {
            public static string Name => "Daylight";
            public static string Version => "0.beta";
        }

        public static class File {
            public static class Path {
                public static string User_Settings => "UserSettings.json";

                public static class Icon {
                    public static class App {
                        public static string Light => "icon/app-light.ico";
                        public static string Dark => "icon/app-dark.ico";
                    }
                }
            }

            public static class Icon {
                public static class App {
                    public static System.Drawing.Icon Light => new(fileName: Path.Icon.App.Light, width: 64, height: 64);
                    public static System.Drawing.Icon Dark => new(fileName: Path.Icon.App.Dark, width: 64, height: 64);
                }
            }

            public static class Log {
                public static string Error => "error.log";
                public static string App => "app.log";
                public static string Debug => "debug.log";
            }
        }

        public static class Registry {
            public static class Path {
                public static string Themes_Personalization => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            }

            public static class Key {
                public static string Apps_Light_Theme => "AppsUseLightTheme";
                public static string System_Light_Theme => "SystemUsesLightTheme";
            }
        }

        public static class Time {
            public static TimeSpan Midnight => new(
                days: 0,
                hours: 0,
                minutes: 0,
                seconds: 0,
                milliseconds: 0
            );

            public static TimeSpan Last_Time_Before_Midnight => new DateTime(
                year: 2,
                month: 1,
                day: 1,
                hour: 0,
                minute: 0,
                second: 0
            ).AddTicks(-1).TimeOfDay;

            public static DateTime Oh_Dark_Thirty_Tomorrow => ExtensionHelper.GenerateDate(_Minute: 30).AddDays(1);

            public static class Format {
                public static string Hour_Minute_Meridiem => "h:mm tt";
                public static string Empty => " ";
            }
        }

        public static class Url {
            public static string IP_Coordinates => "https://ipapi.co/json";
            public static string Sunrise_Sunset => "https://api.sunrise-sunset.org/json";
            public static string Geocoding => "https://nominatim.openstreetmap.org/search";
        }

        public static class Theme {
            public static class Dark {
                public static Color Font => Color.WhiteSmoke;
                public static Color Background => Color.FromArgb(51, 51, 51);
            }

            public static class Light {
                public static Color Font => default(Color);
                public static Color Background => default(Color);
            }
        }
    }
}
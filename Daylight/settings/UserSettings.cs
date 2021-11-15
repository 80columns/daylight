using Daylight.helper;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Daylight.settings {
    public class UserSettings {
        private AppLogLevel p_LogLevel;

        public UserLocationSource LocationSource { get; set; }
        public string CustomLocation { get; set; }
        public string CustomSunriseTime { get; set; }
        public string CustomSunsetTime { get; set; }

        public AppLogLevel LogLevel {
            get {
                #if DEBUG
                    return AppLogLevel.Detailed;
                #else
                    return this.p_LogLevel;
                #endif
            }

            set {
                this.p_LogLevel = value;
            }
        }

        public UserSettings() { }

        public static async Task<UserSettings> CreateAsync() {
            var Configuration_JSON_Str = await File.ReadAllTextAsync(
                path: AppSettings.File.Path.User_Settings,
                encoding: Encoding.UTF8
            );

            return JsonSerializer.Deserialize<UserSettings>(
                json: Configuration_JSON_Str,
                options: GenerateEnumParsingOptions()
            );
        }

        public async Task SaveAsync() {


            var Configuration_JSON_Str = JsonSerializer.Serialize(
                value: this,
                options: GenerateEnumParsingOptions()
            );

            await File.WriteAllTextAsync(
                path: AppSettings.File.Path.User_Settings,
                contents: Configuration_JSON_Str,
                encoding: Encoding.UTF8
            );
        }

        private static JsonSerializerOptions GenerateEnumParsingOptions() {
            var JSON_Options = new JsonSerializerOptions();
            JSON_Options.Converters.Add(new JsonStringEnumConverter());

            return JSON_Options;
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Daylight.model.Tests {
    [TestClass()]
    public class SunriseSunsetTests {
        [TestMethod()]
        public void CreateTest() {
            var JSON = @"{
                ""results"": {
                    ""sunrise"": ""2021-11-15T11:37:45+00:00"",
                    ""sunset"": ""2021-11-15T22:33:08+00:00"",
                    ""solar_noon"": ""2021-11-15T17:05:26+00:00"",
                    ""day_length"": 39323,
                    ""civil_twilight_begin"": ""2021-11-15T11:14:44+00:00"",
                    ""civil_twilight_end"": ""2021-11-15T22:56:08+00:00"",
                    ""nautical_twilight_begin"": ""2021-11-15T10:46:56+00:00"",
                    ""nautical_twilight_end"": ""2021-11-15T23:23:56+00:00"",
                    ""astronomical_twilight_begin"": ""2021-11-15T10:19:30+00:00"",
                    ""astronomical_twilight_end"": ""2021-11-15T23:51:23+00:00""
                },
                ""status"": ""OK""
            }";

            var SunrSuns = SunriseSunset.Create(JSON);

            Assert.AreEqual("2021-11-15T11:37:45+00:00", SunrSuns.results.sunrise);
            Assert.AreEqual("2021-11-15T22:33:08+00:00", SunrSuns.results.sunset);
        }
    }
}
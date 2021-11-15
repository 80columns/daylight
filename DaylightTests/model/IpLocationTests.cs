using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Daylight.model.Tests {
    [TestClass()]
    public class IpLocationTests {
        [TestMethod()]
        public void CreateTest() {
            var JSON = @"{
                ""ip"": ""193.36.224.114"",
                ""version"": ""IPv4"",
                ""city"": ""Miami"",
                ""region"": ""Florida"",
                ""region_code"": ""FL"",
                ""country"": ""US"",
                ""country_name"": ""United States"",
                ""country_code"": ""US"",
                ""country_code_iso3"": ""USA"",
                ""country_capital"": ""Washington"",
                ""country_tld"": "".us"",
                ""continent_code"": ""NA"",
                ""in_eu"": false,
                ""postal"": ""33192"",
                ""latitude"": 25.7689,
                ""longitude"": -80.1946,
                ""timezone"": ""America/New_York"",
                ""utc_offset"": ""-0500"",
                ""country_calling_code"": ""+1"",
                ""currency"": ""USD"",
                ""currency_name"": ""Dollar"",
                ""languages"": ""en-US,es-US,haw,fr"",
                ""country_area"": 9629091.0,
                ""country_population"": 327167434,
                ""asn"": ""AS8100"",
                ""org"": ""ASN-QUADRANET-GLOBAL""
            }";

            var IpLoc = IpLocation.Create(JSON);

            Assert.IsTrue(IpLoc.latitude.HasValue);
            Assert.IsTrue(IpLoc.longitude.HasValue);

            Assert.AreEqual(25.7689m, IpLoc.latitude.Value);
            Assert.AreEqual(-80.1946m, IpLoc.longitude.Value);
            Assert.AreEqual("Miami", IpLoc.city);
            Assert.AreEqual("Florida", IpLoc.region);
            Assert.AreEqual("US", IpLoc.country);

            Assert.AreEqual("25.7689", IpLoc.GetLatitude());
            Assert.AreEqual("-80.1946", IpLoc.GetLongitude());
        }
    }
}
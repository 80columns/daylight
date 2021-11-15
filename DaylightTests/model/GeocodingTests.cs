using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Daylight.model.Tests {
    [TestClass()]
    public class GeocodingTests {
        [TestMethod()]
        public void CreateTest() {
            var JSON = @"{
                ""type"": ""FeatureCollection"",
                ""geocoding"": {
                    ""version"": ""0.1.0"",
                    ""attribution"": ""Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright"",
                    ""licence"": ""ODbL"",
                    ""query"": ""Miami, FL""
                },
                ""features"": [
                    {
                        ""type"": ""Feature"",
                        ""properties"": {
                            ""geocoding"": {
                                ""place_id"": 282510924,
                                ""osm_type"": ""relation"",
                                ""osm_id"": 1216769,
                                ""type"": ""administrative"",
                                ""label"": ""Miami, Miami-Dade County, Florida, United States"",
                                ""name"": ""Miami""
                            }
                        },
                        ""geometry"": {
                            ""type"": ""Point"",
                            ""coordinates"": [
                                -80.19362,
                                25.7741728
                            ]
                        }
                    }
                ]
            }";

            var Geo = Geocoding.Create(JSON);

            Assert.AreEqual(Geo.features.Length, 1);
            Assert.AreEqual(Geo.features[0].geometry.coordinates.Length, 2);

            Assert.AreEqual(-80.19362m, Geo.features[0].geometry.coordinates[0].Value);
            Assert.AreEqual(25.7741728m, Geo.features[0].geometry.coordinates[1].Value);

            Assert.AreEqual("-80.19362", Geo.GetLongitude());
            Assert.AreEqual("25.7741728", Geo.GetLatitude());
        }
    }
}
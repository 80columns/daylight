using System;
using System.Text.Json;

namespace Daylight.model {
    /*
     * Sample object:
     * 
     * {
     *     "type": "FeatureCollection",
     *     "geocoding": {
     *         "version": "0.1.0",
     *         "attribution": "Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright",
     *         "licence": "ODbL",
     *         "query": "Miami, FL"
     *     },
     *     "features": [
     *         {
     *             "type": "Feature",
     *             "properties": {
     *                 "geocoding": {
     *                     "place_id": 282510924,
     *                     "osm_type": "relation",
     *                     "osm_id": 1216769,
     *                     "type": "administrative",
     *                     "label": "Miami, Miami-Dade County, Florida, United States",
     *                     "name": "Miami"
     *                 }
     *             },
     *             "geometry": {
     *                 "type": "Point",
     *                 "coordinates": [
     *                     -80.19362,
     *                     25.7741728
     *                 ]
     *             }
     *         }
     *     ]
     * }
     *
     */

    public class Geocoding {
        public string type { get; set; }
        public GeoSource geocoding { get; set; }
        public GeoFeature[] features { get; set; }

        public static Geocoding Create(
            string _JSON
        ) {
            return JsonSerializer.Deserialize<Geocoding>(_JSON);
        }
        
        public string GetLatitude() {
            return Convert.ToString(this.features[0].geometry.coordinates[1].Value);
        }

        public string GetLongitude() {
            return Convert.ToString(this.features[0].geometry.coordinates[0].Value);
        }
    }

    public class GeoSource {
        public string version { get; set; }
        public string attribution { get; set; }
        public string license { get; set; }
        public string query { get; set; }
    }

    public class GeoFeature {
        public string type { get; set; }
        public GeoProperties properties { get; set; }
        public GeoGeometry geometry { get; set; }
    }

    public class GeoProperties {
        public GeoPlace geocoding { get; set; }
    }

    public class GeoPlace {
        public long? place_id { get; set; }
        public string osm_type { get; set; }
        public long? osm_id { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public string name { get; set; }
    }

    public class GeoGeometry {
        public string type { get; set; }
        public decimal?[] coordinates { get; set; }
    }
}
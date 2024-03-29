﻿using System;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Device.Location;

namespace GeoProximity.Utils
{
    public class GeocodeUtil
    {
        /// <summary>
        /// Call Google Maps API to geocode given location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Tuple<double, double> GetGeoCoordinatesOfLocation(string location)
        {
            if (string.IsNullOrEmpty(location)) throw new ArgumentException("Location is not provided");

            string key = ConfigurationManager.AppSettings["GMapsAPIKey"] ?? string.Empty;
            string uri = ConfigurationManager.AppSettings["GMapsAPIURI"] ?? string.Empty;
            uri += "address=" + location + "&key=" + key;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var geocodeJson = client.GetStringAsync(uri).Result;       
                    var geocodeObj = JsonConvert.DeserializeObject<dynamic>(geocodeJson);
                    double lat = geocodeObj.results[0].geometry.location.lat.Value;
                    double lng = geocodeObj.results[0].geometry.location.lng.Value;

                    return new Tuple<double, double>(lat, lng);
                }
            }
            catch (Exception ex)
            {
                //Log exception for troubleshooting

                //Throw exception for upper layer to handle
                throw new Exception("Unable to geocode given location");
            }
        }

        /// <summary>
        /// Get the distance between given two sets of Latitude and Longitude. 
        /// </summary>
        /// <param name="userLatLng"></param>
        /// <param name="locLatLng"></param>
        /// <returns></returns>
        public static double GetDistance(Tuple<double, double> userLatLng ,Tuple<double, double> locLatLng)
        {
            var userCoordinates = new GeoCoordinate(userLatLng.Item1, userLatLng.Item2);
            var locationCoordinates = new GeoCoordinate(locLatLng.Item1, locLatLng.Item2);

            return Math.Round(userCoordinates.GetDistanceTo(locationCoordinates) / 1000, 2);
        }
    }
}

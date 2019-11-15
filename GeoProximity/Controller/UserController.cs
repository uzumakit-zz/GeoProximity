using System;
using System.Collections.Generic;
using System.Linq;
using GeoProximity.Model.DTO;
using GeoProximity.Model.DAL;
using GeoProximity.Utils;
using Newtonsoft.Json;

namespace GeoProximity.Controller
{
    public class UserController
    {
        /// <summary>
        /// Get User records with Distance, Name, Phone, Company, Address
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Json</returns>
        public string GetUsers(string location)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                Tuple<double, double> geocode = GeocodeUtil.GetGeoCoordinatesOfLocation(location);

                IUserRepository userRepository = new UserRepository();
                List<UserDTO> listOfUserDTO = userRepository.GetUserOrderedByDistance(geocode.Item1, geocode.Item2).ToList();
                
                result.Add("StatusCode", "200");
                result.Add("Message", string.Empty);
                result.Add("Users", listOfUserDTO);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                result.Add("StatusCode", "500");
                result.Add("Message", ex.Message);
                result.Add("Users", new List<UserDTO>());

                return JsonConvert.SerializeObject(result);
            }
        }
    }
}

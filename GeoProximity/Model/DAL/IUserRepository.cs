using System.Collections.Generic;
using GeoProximity.Model.DTO;

namespace GeoProximity.Model.DAL
{
    interface IUserRepository
    {
        IList<UserDTO> GetUserOrderedByDistance(double lat, double lng);
    }
}

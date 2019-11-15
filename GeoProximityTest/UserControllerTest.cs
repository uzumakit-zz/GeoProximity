using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoProximity.Controller;
using Newtonsoft.Json;
using GeoProximity.Model.DTO;

namespace GeoProximityTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void GetUserTestWithNoLocation()
        {
            UserController uc = new UserController();
            string jsonResult = uc.GetUsers(string.Empty);
            var result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            Assert.AreEqual(result.StatusCode.Value, "500");
            StringAssert.Contains(result.Message.Value, "Location");
        }

        [TestMethod]
        public void GetUserTestHasRequiredColumns()
        {
            UserController uc = new UserController();
            string jsonResult = uc.GetUsers("Vancouver");
            var result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            Assert.AreEqual(result.StatusCode.Value, "200", "Request result in Error");

           //TODO: Validate resulting JSON against JSON Schema
        }

        [TestMethod]
        public void GetUserTestHasDataSortedByDistanceAsc()
        {
            UserController uc = new UserController();
            string jsonResult = uc.GetUsers("Vancouver");
            var result = JsonConvert.DeserializeObject<dynamic>(jsonResult);

            double lastDist = result.Users.First.Distance;
            foreach (var user in result.Users)
            {
                double dist = user.Distance.Value;
                Assert.IsTrue(lastDist <= dist, "Data is not Ordered");
                lastDist = dist;
            }
        }


    }
}

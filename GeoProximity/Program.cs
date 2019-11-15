using System;
using GeoProximity.Controller;
using Newtonsoft.Json;
using System.Text;

namespace GeoProximity
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Provide Location for proximity Search:");
                string location = Console.ReadLine();

                UserController uc = new UserController();
                var response = uc.GetUsers(location);
                var userobjs = JsonConvert.DeserializeObject<dynamic>(response);

                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine("Distance | Name | Phone | Company | Address");
                foreach (var user in userobjs.Users)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(user.Distance + "|".PadLeft(10 - user.Distance.Value.ToString().Length, ' '));
                    sb.Append(user.Name + "|".PadLeft(35 - user.Name.Value.Length, ' '));
                    sb.Append(user.Phone + "|".PadLeft(30 - user.Phone.Value.Length, ' '));
                    sb.Append(user.Company + "|".PadLeft(35 - user.Company.Value.Length, ' '));
                    sb.Append(user.Address + "|".PadLeft(200 - user.Address.Value.Length, ' '));
                    Console.WriteLine(sb.ToString());
                }

                Console.WriteLine("---------------------------------------------------------------------------");

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Do you want to continue(y/n): ");
                string res = Console.ReadLine();
                cont = res.ToLower() == "y" ? true : false;
            }            
        }
    }
}

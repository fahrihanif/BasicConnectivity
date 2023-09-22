using BasicConnectivity.Controllers;
using BasicConnectivity.Views;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. Region CRUD");
            Console.WriteLine("2. List all countries");
            Console.WriteLine("3. List all locations");
            Console.WriteLine("4. List regions with Where");
            Console.WriteLine("5. Join tables regions and countries and locations");
            Console.WriteLine("10. Exit");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            choice = Menu(input);
        }
    }

    public static bool Menu(string input)
    {
        switch (input)
        {
            case "1":
                RegionMenu();
                break;
            case "2":
                var country = new Country();
                var countries = country.GetAll();
                //GeneralView.List(countries, "countries");
                break;
            case "3":
                var location = new Location();
                var locations = location.GetAll();
                //GeneralView.List(locations, "locations");
                break;
            case "4":
                var region2 = new Region();
                //string input2 = Console.ReadLine();
                //var result = region2.GetAll().Where(r => r.Name.Contains(input2)).ToList();
                //GeneralView.List(result, "regions");
                break;
            case "5":
                var country3 = new Country();
                var region3 = new Region();
                var location3 = new Location();

                var getCountry = country3.GetAll();
                var getRegion = region3.GetAll();
                var getLocation = location3.GetAll();

                var resultJoin = (from r in getRegion
                                  join c in getCountry on r.Id equals c.RegionId
                                  join l in getLocation on c.Id equals l.CountryId
                                  select new RegionAndCountryVM {
                                      CountryId = c.Id,
                                      CountryName = c.Name,
                                      RegionId = r.Id,
                                      RegionName = r.Name,
                                      City = l.City
                                  }).ToList();

                var resultJoin2 = getRegion.Join(getCountry,
                                                 r => r.Id,
                                                 c => c.RegionId,
                                                 (r, c) => new { r, c })
                                           .Join(getLocation,
                                                 rc => rc.c.Id,
                                                 l => l.CountryId,
                                                 (rc, l) => new RegionAndCountryVM {
                                                     CountryId = rc.c.Id,
                                                     CountryName = rc.c.Name,
                                                     RegionId = rc.r.Id,
                                                     RegionName = rc.r.Name,
                                                     City = l.City
                                                 }).ToList();

                //GeneralView.List(resultJoin2, "regions and countries");
                break;
            case "10":
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }

        return true;
    }

    public static void RegionMenu()
    {
        var region = new Region();
        var regionView = new RegionView();
               
        var regionController = new RegionController(region, regionView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. Insert new region");
            Console.WriteLine("3. Update region");
            Console.WriteLine("4. Delete region");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    regionController.GetAll();
                    break;
                case "2":
                    regionController.Insert();
                    break;
                case "3":
                    regionController.Update();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var region = new Region();
        
        var getAllRegion = region.GetAll();

        if (getAllRegion.Count > 0)
        {
            foreach (var region1 in getAllRegion)
            {
                Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }

        /*var insertResult = region.Insert("Region 5");
        int.TryParse(insertResult, out int result);
        if (result > 0)
        {
            Console.WriteLine("Insert Success");
        }
        else 
        {
            Console.WriteLine("Insert Failed");
            Console.WriteLine(insertResult);
        }*/
    }
}

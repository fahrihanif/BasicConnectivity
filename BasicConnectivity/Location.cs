namespace BasicConnectivity;

public class Location
{
    public int Id { get; set; }
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string CountryId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {StreetAddress} - {PostalCode} - {City} - {StateProvince} - {CountryId}";
    }

    public List<Location> GetAll()
    {
        var locations = new List<Location>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM locations";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    locations.Add(new Location {
                        Id = reader.GetInt32(0),
                        StreetAddress = reader.GetString(1),
                        PostalCode = reader.IsDBNull(2) ? "null" : reader.GetString(2),
                        City = reader.GetString(3),
                        StateProvince = reader.GetString(4),
                        CountryId = reader.GetString(5)
                    });
                }
                reader.Close();
                connection.Close();
                
                return locations;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Location>();
    }
}

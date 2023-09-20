using System.Data.SqlClient;

namespace BasicConnectivity;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    private readonly string connectionString =
        "Data Source=CAMOUFLY;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: Region
    public List<Region> GetAll()
    {
        var regions = new List<Region>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    regions.Add(new Region {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();
                
                return regions;
            }
            reader.Close();
            connection.Close();
            
            return new List<Region>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Region>();
    }

    // GET BY ID: Region
    public Region GetById(int id)
    {
        return new Region();
    }

    // INSERT: Region
    public string Insert(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO regions VALUES (@name);";

        try
        {
            command.Parameters.Add(new SqlParameter("@name", name));

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // UPDATE: Region
    public string Update(int id, string name)
    {
        return "";
    }

    // DELETE: Region
    public string Delete(int id)
    {
        return "";
    }
}

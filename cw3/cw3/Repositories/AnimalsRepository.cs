using System.Data.Common;
using cw3.Model;
using Microsoft.Data.SqlClient;

namespace cw3.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration config)
    {
        _configuration = config;
    }

    public IEnumerable<Animal> GetAnimalsList()
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Id, Name, Description, Category, Area FROM Animal ORDER BY Name";

        var sqlDataReader = cmd.ExecuteReader();
        var animals = new List<Animal>();

        while (sqlDataReader.Read())
        {
            var animal = new Animal()
            {
                Id = (int)sqlDataReader["Id"],
                Name = sqlDataReader["Name"].ToString(),
                Description = sqlDataReader["Description"].ToString(),
                Category = sqlDataReader["Category"].ToString(),
                Area = sqlDataReader["Area"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public int CreateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            "INSERT INTO Animal(Id, Name, Description, Category, Area) VALUES(@Id, @Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@Id", animal.Id);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        return cmd.ExecuteNonQuery();
    }

    public Animal GetAnimal(int animalId)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Id, Name, Description, Category, Area FROM Animal WHERE Id = @Id";
        cmd.Parameters.AddWithValue("@Id", animalId);
        
        var sqlDataReader = cmd.ExecuteReader();
        if (!sqlDataReader.Read()) return null;

        return new Animal()
        {
            Id = (int)sqlDataReader["Id"],
            Name = sqlDataReader["Name"].ToString(),
            Description = sqlDataReader["Description"].ToString(),
            Category = sqlDataReader["Category"].ToString(),
            Area = sqlDataReader["Area"].ToString()
        };
    }

    public int UpdateAnimalInfo(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            "UPDATE Animal SET Id=@Id, Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE Id=@Id";
        cmd.Parameters.AddWithValue("@Id", animal.Id);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        return cmd.ExecuteNonQuery();
    }

    public int DeleteAnimalById(int animalId)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Animal WHERE Id=@Id";
        cmd.Parameters.AddWithValue("@Id", animalId);

        return cmd.ExecuteNonQuery();
    }
}
using Etterem.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Etterem.Controllers
{
    [ApiController]
    [Route("rendeles")]
    public class RendelesController : ControllerBase
    {
        private const string ConnectionString = "Server=localhost;Port=3306;Database=etterem;User ID=root;Password=;";

        public static Rendeles ReadRendeles(MySqlDataReader reader)
        {
            Rendeles rendeles = new Rendeles
            {
                Id = reader.GetInt32("id"),
                Dish = reader.GetString("dish"),
                Description = reader.GetString("description"),
                OrderTime = reader.GetDateTime("orderTime"),
                UpdateTime = reader.GetDateTime("updateTime"),
                VendegId = reader.GetInt32("vendegId")
            };
            return rendeles;
        }

        [HttpGet]
        public List<Rendeles> GetAll()
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT * FROM rendeles", connection);
            using var reader = command.ExecuteReader();

            var result = new List<Rendeles>();
            while (reader.Read())
            {
                result.Add(ReadRendeles(reader));
            }
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Rendeles> GetById(int id)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT * FROM rendeles WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return NotFound();
            }
            return ReadRendeles(reader);
        }

        [HttpPost]
        public IActionResult Create(RendelesDto rendeles)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("INSERT INTO rendeles (dish, description, orderTime, updateTime, vendegId) VALUES (@dish, @description, NOW(), NOW(), @vendegId)", connection);
            command.Parameters.AddWithValue("@dish", rendeles.Dish);
            command.Parameters.AddWithValue("@description", rendeles.Description);
            command.Parameters.AddWithValue("@vendegId", rendeles.VendegId);
            command.ExecuteNonQuery();

            return Ok(new { id = command.LastInsertedId });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, RendelesDto rendeles)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("UPDATE rendeles SET dish = @dish, description = @description, vendegId = @vendegId, updateTime = NOW() WHERE id = @id", connection);
            command.Parameters.AddWithValue("@dish", rendeles.Dish);
            command.Parameters.AddWithValue("@description", rendeles.Description);
            command.Parameters.AddWithValue("@vendegId", rendeles.VendegId);
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() == 0 ? NotFound() : Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("DELETE FROM rendeles WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() == 0 ? NotFound() : Ok();
        }

        [HttpGet("vendeg/{vendegId}")]
        public IActionResult GetVendeg(int vendegId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand(@"SELECT name, email FROM vendeg WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", vendegId);
            using var reader = command.ExecuteReader();

            if (!reader.Read()) return NotFound();

            var eredmeny = new
            {
                name = reader.GetString("name"),
                email = reader.GetString("email")
            };
            return Ok(eredmeny);
        }

        [HttpGet("vendeg/{vendegId}/rendelesek")]
        public IActionResult GetVendegRendelesei(int vendegId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand(@"SELECT vendeg.name, rendeles.dish, rendeles.description FROM vendeg INNER JOIN rendeles ON rendeles.vendegId = vendeg.id WHERE vendeg.id = @id", connection);
            command.Parameters.AddWithValue("@id", vendegId);
            using var reader = command.ExecuteReader();

            var eredmeny = new List<object>();
            while (reader.Read())
            {
                eredmeny.Add(new
                {
                    name = reader.GetString("name"),
                    dish = reader.GetString("dish"),
                    description = reader["description"].ToString()
                });
            }
            return Ok(eredmeny);
        }

        [HttpGet("count")]
        public int GetCount()
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT COUNT(*) AS darab FROM rendeles", connection);
            using var reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32("darab");
        }

        [HttpGet("vendeg/{vendegId}/count")]
        public int GetVendegCount(int vendegId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT COUNT(*) AS darab FROM rendeles WHERE vendegId = @id", connection);
            command.Parameters.AddWithValue("@id", vendegId);
            using var reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32("darab");
        }
    }
}

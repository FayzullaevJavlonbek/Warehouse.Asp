using Npgsql;
using System.Data;
using Warehouse.DbConstants;
using Warehouse.Models;

namespace Warehouse.Services
{
    public class ProductService : IProductService
    {
        private static readonly string _connection = "Host=localhost; Port=5432; Database=main-warehouse-db; User Id = postgres; Password = 2001;";

        public async Task<int> Create(Product product)
        {
            if(product.Id == 0)
            {
                await using var connection = new NpgsqlConnection(_connection);
                connection.Open();
                await using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO public.products(name, price, amount)" +
                                        "VALUES (@name, @price, @amount) RETURNING id;";
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@amount", product.Amount);
                product.Id = (int)await command.ExecuteScalarAsync();
            }   
            return product.Id;
        }

        public async Task<Product> Read(int id)
        {
            await using NpgsqlConnection connection=new NpgsqlConnection(_connection);
            connection.Open();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT id, name, price, amount, total " +
                "FROM public.products; " +
                "WHERE id = @id";
            command.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if(reader.HasRows)
            {
                reader.Read();
                string name = (string)reader["name"];
                double price = (double)reader["price"];
                int amount = (int)reader["amount"];
                double total = (double)reader["total"];
                return new Product(id, name, price, amount, total);

            }
            return null;
        }

        public async Task<bool> Update(Product product)
        {
            int result = 0;
            if(product.Id>0) 
            {
                await using NpgsqlConnection connection= new NpgsqlConnection(_connection);
                connection.Open();
                await using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE public.products " +
                    "SET name=@name, price=@price, amount=@amount " +
                    "WHERE id = @id;";
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@amount", product.Amount);
                command.Parameters.AddWithValue("@id", product.Id);
                result = await command.ExecuteNonQueryAsync();
            }
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(_connection);
            connection.Open();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM public.products " +
                " WHERE id = @id;";
            command.Parameters.AddWithValue("@id", id);
            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}

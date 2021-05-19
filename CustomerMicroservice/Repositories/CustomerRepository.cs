using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Microservice.Repositories
{
    public class CustomerRepository
    {
        private string ConnectionString { get; }

        public CustomerRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Models.Customer>> GetAll()
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                var sqlQuery = "SELECT * FROM Customer";
                await connection.OpenAsync();
                return await connection.QueryAsync<Models.Customer>(sqlQuery);
            }
        }

        public async Task<Models.Customer> GetById(int id)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                var sqlQuery = "SELECT * FROM Customer WHERE Id = @Id";
                await connection.OpenAsync();
                return await connection.QuerySingleAsync<Models.Customer>(sqlQuery, new { Id = id });
            }
        }

        public async Task<Models.Customer> Add(Models.Customer customer)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                var sqlQuery = "INSERT INTO Customer (Name, Address, Telephone, Email) Values(@Name, @Address, @Telephone, @Email)";
                await connection.OpenAsync();
                await connection.ExecuteAsync(sqlQuery, customer);
            }
            return customer;
        }

        public void Update(Models.Customer customer)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                var sqlQuery = "UPDATE Customer SET Name = @Name, Address = @Address, Email = @Email WHERE Id = @Id";
                connection.OpenAsync();
                connection.ExecuteAsync(sqlQuery, customer);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                var sqlQuery = "DELETE FROM Customer WHERE Id = @Id";
                connection.OpenAsync();
                connection.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private string ConnectionString { get; }
        //private readonly string connectionString =  "Data Source=DESKTOPMORGANJA;Initial Catalog=CustomerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IEnumerable<Models.Customer>> GetAll()
        {
            IEnumerable<Models.Customer> customers;

            using (SqlConnection connection = new(ConnectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "SELECT * FROM Customer";
                customers = await connection.QueryAsync<Models.Customer>(sqlQuery);
            }

            return customers;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<Models.Customer> Get(int id)
        {
            Models.Customer customer = new();
            using (SqlConnection connection = new(ConnectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "SELECT * FROM Customer WHERE Id = @Id";
                customer = await connection.QuerySingleAsync<Models.Customer>(sqlQuery, new { Id = id });
            }

            return customer;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Customer customer)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "INSERT INTO Customer (Name, Address, Telephone, Email) Values(@Name, @Address, @Telephone, @Email)";
                await connection.ExecuteAsync(sqlQuery, customer);
            }
            return Ok(customer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Customer customer)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "UPDATE Customer SET Name = @Name, Address = @Address, Email = @Email WHERE Id = @Id";
                await connection.ExecuteAsync(sqlQuery, customer);
            }
            return Ok();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (SqlConnection connection = new(ConnectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "DELETE FROM Customer WHERE Id = @Id";
                await connection.ExecuteAsync(sqlQuery, new { Id = id });
            }
            return Ok();
        }
    }
}
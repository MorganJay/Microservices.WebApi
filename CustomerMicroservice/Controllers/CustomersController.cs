using Customer.Microservice.Repositories;
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
        private readonly CustomerRepository productRepository;

        public CustomersController(IConfiguration configuration)
        {
            productRepository = new CustomerRepository(configuration);
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await productRepository.GetAll());
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            return Ok(await productRepository.GetById(id));
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                var model = await productRepository.Add(customer);
                return Ok(model);
            }
            return BadRequest();
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = id;
                productRepository.Update(customer);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await productRepository.GetById(id);
            if (customer is null) return NotFound();

            productRepository.Delete(id);
            return Ok();
        }
    }
}
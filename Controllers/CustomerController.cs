using Microsoft.AspNetCore.Mvc;
using Advantage.API;
using Advantage.API.Models;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ApiContext _ctx;
        public CustomerController(ApiContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Customers.OrderBy(c => c.Id);
            return Ok(data);
        }

        //GET api/customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _ctx.Customers.Find(id);
            return Ok(customer);
        }
        // 12:33 ep 15
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }
    }
}

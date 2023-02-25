using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sqrs_concepts.Domain.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sqrs_concepts.Controllers
{
    [Route("api/[controller]")]
    public class OrderssController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrderssController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] OrderDto input)
        {
            var order = new Order(Guid.NewGuid(), input.MarketId, input.SelectionId, input.Price,
                input.Size, input.Side);
            order = await orderRepository.SaveAsync(order);
            return Ok(order);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


using Clients.Model;
using Microsoft.AspNetCore.Mvc;
using Clients.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await Order_Dal.GetAll();
        }
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]

        // POST api/<OrdersController>
        [HttpPost]
        public async Task Post([FromBody] Order curOrder)
        {
            await Order_Dal.Insert(curOrder);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Order curorder)
        {
            await Order_Dal.Update(id, curorder);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await OrderProduct_Dal.Delete(id);
            await Order_Dal.Delete(id);
        }
        [HttpGet("GetOrderWithMaxId")]
        public async Task<Order> GetOrderWithMaxId()
        {
            return await Order_Dal.GetOrderWithMaxId();
        }

        // POST api/<OrderController>
        [HttpPost("OrderProducts")]
        public async Task Post([FromBody] List<OrderProduct> curOrderProducts)
        {
            await OrderProduct_Dal.Insert(curOrderProducts);
        }
        [HttpGet("OrderProducts/{curOrderId}")]
        public async Task<IEnumerable<OrderProduct>> GetOrderProducts(int curOrderId)
        {
            return await OrderProduct_Dal.GetAll(curOrderId);
        }
        [HttpPut("OrderProducts")]
        public async Task Put([FromBody] List<OrderProduct> curOrderProducts)
        {
            await OrderProduct_Dal.Update(curOrderProducts);
        }
    }
}

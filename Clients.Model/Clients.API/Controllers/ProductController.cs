using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clients.Data;
using Clients.Model;

namespace Clients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await Product_Dal.GetAll();
        }
        // GET api/<ClientsController>/5
        [HttpGet("{id}")]

        // POST api/<ClientsController>
        [HttpPost]
        public async Task Post([FromBody] Product curProduct)
        {
            await Product_Dal.Insert(curProduct);
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Product curproduct)
        {
            await Product_Dal.Update(id, curproduct);
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await Product_Dal.Delete(id);
        }
        [HttpPut("UpdateProducts")]
        public async Task UpdateProducts([FromBody] List<Product> Products)
        {
            await Product_Dal.UpdateProducts(Products);
        }
    }
}

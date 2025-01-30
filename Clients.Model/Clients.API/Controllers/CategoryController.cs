using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clients.Data;
using Clients.Model;

namespace Clients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await Category_Dal.GetAll();
        }
        // GET api/<ClientsController>/5
        [HttpGet("{id}")]

        // POST api/<ClientsController>
        [HttpPost]
        public async Task Post([FromBody] Category curCategory)
        {
            await Category_Dal.Add(curCategory);
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Category curcategory)
        {
            await Category_Dal.Update(id, curcategory);
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await Category_Dal.Delete(id);
        }
    }
}

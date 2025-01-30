using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clients.Data;
using Clients.Model;

namespace Clients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<City>> Get()
        {
            return await City_Dal.GetAll();
        }
        // GET api/<ClientsController>/5
        [HttpGet("{id}")]

        // POST api/<ClientsController>
        [HttpPost]
        public async Task Post([FromBody] City curCity)
        {
            await City_Dal.Add(curCity);
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] City curcity)
        {
            await City_Dal.Update(id, curcity);
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await City_Dal.Delete(id);
        }
    }
}

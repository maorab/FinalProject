using Clients.Model;
using Microsoft.AspNetCore.Mvc;
using Clients.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await Client_Dal.GetAll();
        }
        // GET api/<ClientsController>/5
        [HttpGet("{id}")]

        // POST api/<ClientsController>
        [HttpPost]
        public async Task Post([FromBody] Client curClient)
        {
            await Client_Dal.Add(curClient);
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Client curclient)
        {
            await Client_Dal.Update(id,curclient);
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await Client_Dal.Delete(id);
        }
    }
}

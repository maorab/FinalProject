using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clients.Data;
using Clients.Model;

namespace Clients.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await Company_Dal.GetAll();
        }
        // GET api/<ClientsController>/5
        [HttpGet("{id}")]

        // POST api/<ClientsController>
        [HttpPost]
        public async Task Post([FromBody] Company curCompany)
        {
            await Company_Dal.Add(curCompany);
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Company curcompany)
        {
            await Company_Dal.Update(id, curcompany);
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await Company_Dal.Delete(id);
        }
    }
}

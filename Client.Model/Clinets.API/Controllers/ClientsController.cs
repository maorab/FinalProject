using Microsoft.AspNetCore.Mvc;
using Clients.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private static List<Client> clients = new List<Client>();
        private static void AddClients()
        {
            clients.Add(new Client(1, "Maor" ,"Abiri", 2006));
        }
        static ClientsController()
        {
            AddClients();
        }
        // GET: api/<ClientsController>
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return clients;
        }
            

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public Client? Get(int id)
        {
            foreach (Client client in clients)
            {
                if (client.Id == id)
                    return client;
            }
            return null;
        }

        // POST api/<ClientsController>
        [HttpPost]
        public void Post([FromBody] Client curclient)
        {
            int id = curclient.Id + 1;
            //curclient.Id = curclient.Id;
            clients.Add(new Client(id, curclient.FirstName, curclient.LastName, curclient.BirthYear));
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Client curclient)
        {
            foreach (Client client in clients)
            {
                if(client.Id == curclient.Id)
                {
                    client.FirstName = curclient.FirstName;
                    client.LastName = curclient.LastName;
                    client.BirthYear = curclient.BirthYear;
                        
                }
                return;
            }
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            foreach (Client client in clients)
                if (client.Id == id)
                {
                    clients.Remove(client);
                    return;
                }
        }
    }
}

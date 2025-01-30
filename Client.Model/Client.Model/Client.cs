using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Model
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BirthYear { get; set; }
        public Client(int id , string firstname , string lastname, int birthyear) 
        {
            Id = id ;
            FirstName = firstname ;
            LastName = lastname ;
            BirthYear = birthyear ;

        }

    }
}

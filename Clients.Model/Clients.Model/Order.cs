using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Clients.Model
{
    public class Order
    {
        public int Id { get; set; }


        //[Required(ErrorMessage = "Note is required")]
        //[MinLength(1, ErrorMessage = "Note must be at least 1 characters long")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Note must contain only")]
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Client Client { get; set; } = new Client();
        public Order(int id,DateTime date,Client client,string note)
        {
            Id = id;
            Date = date;
            Client = client;
            Note = note;

        }
        public Order() { }
    }
}

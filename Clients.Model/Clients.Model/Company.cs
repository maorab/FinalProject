using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Model
{
    public class Company
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Company name is required")]
        //[MinLength(2, ErrorMessage = "Company name must be at least 2 characters long")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Company name must contain only")]
        public string? Name { get; set; }
        public override string ToString() { return Name; }
        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Company() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Model
{
    public class City
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "City name is required")]
        //[MinLength(2, ErrorMessage = "City name must be at least 2 characters long")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "City name must contain only")]
        public string? Name { get; set; }
        public override string ToString() { return Name; }
        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public City() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clients.Model
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public Category Category { get; set; } = new Category();
    }
}

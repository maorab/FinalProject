using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Model
{
    public class Product
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "name is required")]
        //[MinLength(2, ErrorMessage = "name must be at least 2 characters long")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only")]
        public string Name { get; set; }

        public Category Category { get; set; } = new Category();
        public Company Company { get; set; } = new Company();

        public int Price { get; set; }
        public string Description { get; set; }

        public string? Picture { get; set; }

        public int Count { get; set; }
        public Product(int id, string name, Category category, Company company, int price, string description, string picture, int count)
        {
            Id = id;
            Name = name;
            Category = category;
            Company = company;
            Price = price;
            Description = description;
            Picture=picture;
            Count=count;
        }
        public override bool Equals(object obj)
        {
            return this.Id == (obj as Product).Id;
        }
        public Product() { }
    }
}

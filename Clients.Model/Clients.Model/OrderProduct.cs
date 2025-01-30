using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Clients.Model
{
    public class OrderProduct
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Quantity is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Price { get; set; }
        public OrderProduct() { }
        public OrderProduct(Order curOrder, Product curProduct,int price,int quantity)
        {
            Order = curOrder;
            Product = curProduct;
            Price = price;
            Quantity = quantity;
        }
        //public OrderProduct(int id, Product product, Category category, Company company)
        //{
        //    Id = id;
        //    Product = product;
        //    Product.Category = category;
        //    Product.Company = company;
        //}
    }
}

using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class OrderProduct_Dal
    {
        const string TABLE_NAME = "Table_OrderProduct";

        public static async Task<IEnumerable<OrderProduct>> GetAll(int curOrderId)
        {
            string sql = $@"
               SELECT {TABLE_NAME}.[Id], {TABLE_NAME}.[Price], {TABLE_NAME}.[Quantity], 
                      Table_Order.[Id], Table_Order.[Date],
                      Table_Product.[Id], Table_Product.[Name], Table_Product.[Description], Table_Product.[Price], Table_Product.[Picture], 
                      Table_Category.[Id], Table_Category.[Name],
                      Table_Company.[Id], Table_Company.[Name]
               FROM {TABLE_NAME}
                   INNER JOIN Table_Order ON {TABLE_NAME}.[Order] = Table_Order.[Id] 
                   INNER JOIN Table_Product ON {TABLE_NAME}.[Product] = Table_Product.[Id]
                   INNER JOIN Table_Category ON Table_Product.[Category] = Table_Category.[Id] 
                   INNER JOIN Table_Company ON Table_Product.[Company] = Table_Company.[Id]
                   WHERE Table_OrderProduct.[Order] = {curOrderId}";

            // Execute the query and retrieve the data - using the Dal class
            var orderProducts = await Dal.QuerySql<OrderProduct>(
                    sql,
                    new Type[] { typeof(OrderProduct), typeof(Order), typeof(Product), typeof(Category), typeof(Company) },
                      objects =>
                      {
                          var orderProduct = (OrderProduct)objects[0];
                          var order = (Order)objects[1];
                          var product = (Product)objects[2];
                          var category = (Category)objects[3];
                          var company = (Company)objects[4];
                          orderProduct.Order = order;
                          orderProduct.Product = product;
                          product.Category = category;
                          product.Company = company;
                          return orderProduct;
                      },
                    splitOn: "Id"
            );
            // Return the list of orderProducts
            return orderProducts;
        }
        public static async Task Insert(OrderProduct curOrderProduct)
        {
            string sql = "INSERT INTO Table_OrderProduct ([Order],[Product],[Quantity],[Price]) VALUES(@OrderId, @ProductId,@Quantity,@Price)";

            await Dal.ExecuteSql(sql, new { OrderId = curOrderProduct.Order.Id, ProductId = curOrderProduct.Product.Id, curOrderProduct.Quantity, curOrderProduct.Price });
        }
        public static async Task Insert(List<OrderProduct> curOrderProducts)
        {
            foreach (OrderProduct curOrderProduct in curOrderProducts) await Insert(curOrderProduct);
        }
        public static async Task Delete(int curOrderId)
        {
            string sql = $"DELETE FROM {TABLE_NAME} WHERE {TABLE_NAME}.[Order] = @OrderId";
            await Dal.ExecuteSql(sql, new { OrderId = curOrderId });
        }
        public static async Task Update(List<OrderProduct> curOrderProducts)
        {
            Order curOrder = curOrderProducts[0].Order;
            await Delete(curOrder.Id);

            await Insert(curOrderProducts);
        }
    }
}


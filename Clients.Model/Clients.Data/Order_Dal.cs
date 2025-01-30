using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class Order_Dal
    {
        const string TABLE_NAME = "Table_Order";

        public static async Task<IEnumerable<Order>> GetAll()
        {
            string sql = $@"
                        SELECT {TABLE_NAME}.[Id], {TABLE_NAME}.[Date], {TABLE_NAME}.[Note],
                            Table_Client.[Id], Table_Client.[FirstName], Table_Client.[LastName], Table_Client.[BirthYear],
                            Table_City.[Id], Table_City.[Name]
                        FROM {TABLE_NAME}
                        INNER JOIN Table_Client ON {TABLE_NAME}.[Client] = Table_Client.[Id]
                        INNER JOIN Table_City ON Table_Client.[City] = Table_City.[Id]";


            // Execute the query with explicit splitOn using the correct aliases
            var orders = await Dal.QuerySql<Order>(
                  sql,
                  new Type[] { typeof(Order), typeof(Client), typeof(City) },
                  objects =>
                  {
                      var order = (Order)objects[0];
                      var client = (Client)objects[1];
                      var city = (City)objects[2];

                      client.City = city;
                      order.Client = client;

                      return order;
                  },
                  splitOn: "Id,Id"
              );

            return orders;
        }

        public static async Task Insert(Order curOrder)
        {
            string sql = "INSERT INTO Table_Order (Date, Client, Note) VALUES(@Date, @ClientId,@Note)";

            await Dal.ExecuteSql(sql, new { curOrder.Date, ClientId = curOrder.Client.Id, curOrder.Note });
        }
        public static async Task Update(int id, Order curOrder)
        {
            string sql = $@"
        UPDATE {TABLE_NAME} 
        SET 
            Date = @Date, 
            Client = @ClientId, 
            Note = @Note
        WHERE Id = @Id";

            // Convert first letter of FirstName and LastName to uppercase

            int ClientId = curOrder.Client.Id;
            await Dal.ExecuteSql(sql, new
            {
                curOrder.Date,
                curOrder.Note,
                ClientId,
                curOrder.Id
            });
        }
        public static async Task Delete(int id)
        {
            string sql = "DELETE FROM Table_Order WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { Id = id });
        }

        public static async Task<Order> GetOrderWithMaxId()
        {
            IEnumerable<Order> orders = await GetAll();
            Order maxIdOrder = new Order();
            foreach (Order order in orders)
                if (order.Id > maxIdOrder.Id) maxIdOrder = order;
            return maxIdOrder;
        }
    }
}

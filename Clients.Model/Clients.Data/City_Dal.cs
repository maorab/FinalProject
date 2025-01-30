using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class City_Dal
    {
        public static async Task<IEnumerable<City>> GetAll()
        {
            // The SQL query to select all clients
            string sql = $"SELECT * FROM Table_City";
            // Execute the query and retrieve the data - using the Dal class
            var items = await Dal.QuerySql<City>(sql);
            // Return the list of items
            return items;
        }
        public static async Task Add(City curClient)
        {
            string sql = "INSERT INTO Table_City (Name) VALUES(@Name)";

            await Dal.ExecuteSql(sql, new { curClient.Name });

        }
        public static async Task Update(int id, City curClient)
        {
            string sql = "UPDATE Table_City SET Name = @Name WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { curClient.Name, id });
        }

        public static async Task Delete(int id)
        {
            string sql = "DELETE FROM Table_City WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { Id = id });
        }
    }
}

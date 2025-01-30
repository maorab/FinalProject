using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class Category_Dal
    {
        public static async Task<IEnumerable<Category>> GetAll()
        {
            // The SQL query to select all clients
            string sql = $"SELECT * FROM Table_Category";
            // Execute the query and retrieve the data - using the Dal class
            var items = await Dal.QuerySql<Category>(sql);
            // Return the list of items
            return items;
        }
        public static async Task Add(Category curCategory)
        {
            string sql = "INSERT INTO Table_Category (Name) VALUES(@Name)";

            await Dal.ExecuteSql(sql, new { curCategory.Name });

        }
        public static async Task Update(int id, Category curCategory)
        {
            string sql = "UPDATE Table_Category SET Name = @Name WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { curCategory.Name, id });
        }

        public static async Task Delete(int id)
        {
            string sql = "DELETE FROM Table_Category WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { Id = id });
        }
    }
}

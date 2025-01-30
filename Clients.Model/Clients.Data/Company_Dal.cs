using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class Company_Dal
    {
        public static async Task<IEnumerable<Company>> GetAll()
        {
            // The SQL query to select all clients
            string sql = $"SELECT * FROM Table_Company";
            // Execute the query and retrieve the data - using the Dal class
            var items = await Dal.QuerySql<Company>(sql);
            // Return the list of items
            return items;
        }
        public static async Task Add(Company curCompany)
        {
            string sql = "INSERT INTO Table_Company (Name) VALUES(@Name)";

            await Dal.ExecuteSql(sql, new { curCompany.Name });

        }
        public static async Task Update(int id, Company curCompany)
        {
            string sql = "UPDATE Table_Company SET Name = @Name WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { curCompany.Name, id });
        }

        public static async Task Delete(int id)
        {
            string sql = "DELETE FROM Table_Company WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { Id = id });
        }
    }
}

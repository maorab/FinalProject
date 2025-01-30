using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class Client_Dal
    {
        const string TABLE_NAME = "Table_Client";
        public static async Task<IEnumerable<Client>> GetAll()
        {
            string sql = $@"SELECT {TABLE_NAME}.Id, {TABLE_NAME}.FirstName,{TABLE_NAME}.LastName, {TABLE_NAME}.BirthYear,{TABLE_NAME}.Gender,{TABLE_NAME}.Email,Table_City.Id, Table_City.Name FROM {TABLE_NAME} INNER JOIN Table_City ON {TABLE_NAME}.City = Table_City.Id";

            // Execute the query and retrieve the data - using the Dal class
            var clients = await Dal.QuerySql<Client>(sql, new Type[] { typeof(Client), typeof(City) },
            objects =>
            {
                var client = (Client)objects[0];
                var city = (City)objects[1];
                client.City = city;
                return client;
            },
            splitOn: "Id"
            );
            // Return the list of clients
            return clients;
        }
        public static async Task Add(Client curClient)
        {
            string sql = "INSERT INTO Table_Client (FirstName, LastName, BirthYear,Gender,Email,City) VALUES(@FirstName, @LastName, @BirthYear,@Gender,@Email,@CityId)";

            await Dal.ExecuteSql(sql, new { curClient.FirstName, curClient.LastName, curClient.BirthYear, curClient.Gender, curClient.Email, CityId = curClient.City.Id });

        }

        public static async Task Update(int id, Client curClient)
        {
            string sql = $@"
        UPDATE {TABLE_NAME} 
        SET 
            FirstName = @FirstName, 
            LastName = @LastName, 
            BirthYear = @BirthYear, 
            Gender = @Gender,Email=@Email , 
            City = @CityId
        WHERE Id = @Id";

            // Convert first letter of FirstName and LastName to uppercase

            int CityId = curClient.City.Id;
            await Dal.ExecuteSql(sql, new
            {
                curClient.FirstName,
                curClient.LastName,
                curClient.BirthYear,
                curClient.Gender,
                curClient.Email,
                CityId, // Include the City Id
                curClient.Id // Identify which client to update
            });
        }

        public static async Task Delete(int id)
        {
            string sql = "DELETE FROM Table_Client WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { Id = id });
        }
    }

}

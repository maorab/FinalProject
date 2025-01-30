using Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data
{
    public class Product_Dal
    {
        const string TABLE_NAME = "Table_Product";
        public static async Task<IEnumerable<Product>> GetAll()
        {
            string sql = $@"SELECT {TABLE_NAME}.Id, {TABLE_NAME}.Name, {TABLE_NAME}.Description,
{TABLE_NAME}.Price,{TABLE_NAME}.Picture, {TABLE_NAME}.[Count],
Table_Category.Id, Table_Category.Name,
Table_Company.Id, Table_Company.Name
FROM {TABLE_NAME}
INNER JOIN Table_Category ON {TABLE_NAME}.Category = Table_Category.Id
INNER JOIN Table_Company ON {TABLE_NAME}.Company = Table_Company.Id";

            // Execute the query and retrieve the data - using the Dal class
            var products = await Dal.QuerySql<Product>(

            sql, new Type[] { typeof(Product), typeof(Category), typeof(Company) },
            objects =>
            {
                var product = (Product)objects[0];
                var category = (Category)objects[1];
                var company = (Company)objects[2];
                product.Category = category;
                product.Company = company;

                return product;
            },
            splitOn: "Id"
            );
            // Return the list of products
            return products;
        }
        public static async Task Insert(Product curProduct)
        {
            string sql = "INSERT INTO Table_Product ([Name], [Category], [Company],[Price],[Description],[Picture],[Count]) VALUES(@Name, @CategoryId, @CompanyId,@Price,@Description,@Picture,@Count)";

            await Dal.ExecuteSql(sql, new { curProduct.Name, CategoryId = curProduct.Category.Id, CompanyId = curProduct.Company.Id, curProduct.Price, curProduct.Description, curProduct.Picture, curProduct.Count });

        }

        public static async Task Update(int id, Product curProduct)
        {
            string sql = $@"
        UPDATE {TABLE_NAME} 
        SET 
            [Name] = @Name, 
            [Category] = @CategoryId, 
            [Company] = @CompanyId, 
            [Price] = @Price,
            [Description]=@Description,
            [Picture] = @Picture, 
            [Count] = @Count
            WHERE Id = @Id";


            int CompanyId = curProduct.Company.Id;
            int CategoryId = curProduct.Category.Id;
            await Dal.ExecuteSql(sql, new
            {
                curProduct.Name,
                CategoryId,
                CompanyId,
                curProduct.Price,
                curProduct.Description,
                curProduct.Picture,
                curProduct.Count,
                curProduct.Id // Identify which Product to update
            });
        }

        public static async Task Delete(int id)
        {
            string sql = "DELETE FROM Table_Product WHERE Id = @Id";
            await Dal.ExecuteSql(sql, new { Id = id });
        }
        public static async Task UpdateProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                Update(product.Id, product);
            }
        }
    }
}









//using Clients.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Clients.Data
//{
//    public class Product_Dal
//    {
//        const string TABLE_NAME = "Table_Product";

//        public static async Task<IEnumerable<Product>> GetAll()
//        {
//            string sql = $@"SELECT {TABLE_NAME}.Id, {TABLE_NAME}.Name, {TABLE_NAME}.Description,{TABLE_NAME}.Picture, {TABLE_NAME}.Price,
//                        Table_Category.Id, Table_Category.Name,
//                        Table_Company.Id, Table_Company.Name
//                      FROM {TABLE_NAME}
//                        INNER JOIN Table_Category ON {TABLE_NAME}.Category = Table_Category.Id
//                        INNER JOIN Table_Company ON {TABLE_NAME}.Company = Table_Company.Id";

//            // Execute the query and retrieve the data - using the Dal class
//            var products = await Dal.QuerySql<Product>(

//            sql, new Type[] { typeof(Product), typeof(Category), typeof(Company) },
//            objects =>
//            {
//                var product = (Product)objects[0];
//                var category = (Category)objects[1];
//                var company = (Company)objects[2];
//                product.Category = category;
//                product.Company = company;

//                return product;
//            },
//            splitOn: "Id"
//            );
//            // Return the list of products
//            return products;
//        }




//        // Add a new client
//        public static async Task Insert(Product curProduct)
//        {
//            string sql = $@" INSERT INTO {TABLE_NAME} 
//        (Name, Price, Description, Picture, Category, Company) 
//        VALUES 
//        (@Name, @Price, @Description, @Picture, @CategoryId, @CompanyId)";

//            // Convert first letter of FirstName and LastName to uppercase
//            curProduct.Name = char.ToUpper(curProduct.Name[0]) + curProduct.Name.Substring(1).ToLower();

//            int CategoryId = curProduct.Category.Id;
//            int CompanyId = curProduct.Company.Id;
//            await Dal.ExecuteSql(sql, new
//            {
//                curProduct.Name,
//                curProduct.Price,
//                curProduct.Description,
//                curProduct.Pictrue,
//                CategoryId,
//                CompanyId
//            });
//        }

//        public static async Task Update(int id, Product curProduct)
//        {
//            string sql = $@"UPDATE {TABLE_NAME} SET 
//            Name = @Name, 
//            Price = @Price, 
//            Description = @Description,
//            Picture = @Picture, 
//            Category = @CategoryId,
//            Company = @CompanyId
//        WHERE Id = @Id";

//            // Convert first letter of FirstName and LastName to uppercase
//            curProduct.Name = char.ToUpper(curProduct.Name[0]) + curProduct.Name.Substring(1).ToLower();

//            int CategoryId = curProduct.Category.Id;
//            int CompanyId = curProduct.Company.Id;
//            int Id = curProduct.Id;
//            await Dal.ExecuteSql(sql, new
//            {
//                curProduct.Name,
//                curProduct.Price,
//                curProduct.Description,
//                curProduct.Pictrue,
//                CategoryId,
//                CompanyId,
//                Id // Identify which Product to update
//            });
//        }



//        // Delete a Product by ID
//        public static async Task Delete(int id)
//        {
//            string sql = $"DELETE FROM {TABLE_NAME} WHERE Id = @Id";
//            await Dal.ExecuteSql(sql, new { Id = id });
//        }

//    }
//}

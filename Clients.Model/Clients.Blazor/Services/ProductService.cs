using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using Clients.Model;

namespace Clients.Blazor.Services
{
    public class ProductService
    {
        const string URL = "https://localhost:7190/api/Product";

        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>?> Read()
        {

            // קבלת תשובה משירות זה ושמירה שלה

            var response = await _httpClient.GetAsync(URL);
            response.EnsureSuccessStatusCode();

            // קריאה של התשובה כמחרוזת JSON, הוספת המרה מותאמת לרשימה של אובייקטים מסוג Country

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy =JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Deserialize<List<Product>>(jsonString, options);
        }
        public async Task<bool> Create(Product curProduct)
        {
            var content = new StringContent(JsonSerializer.Serialize(curProduct),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Update(Product curProduct)
        {
            var content = new StringContent(JsonSerializer.Serialize(curProduct),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{URL}/{curProduct.Id}", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{URL}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProducts(List<Product> Products)
        {
            var content = new StringContent(JsonSerializer.Serialize(Products),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{URL}/UpdateProducts", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }

    }
}

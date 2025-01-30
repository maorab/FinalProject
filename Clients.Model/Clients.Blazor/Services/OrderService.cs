using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using Clients.Model;
using Microsoft.Extensions.Options;


namespace Clients.Blazor.Services
{
    public class OrderService
    {
        const string URL = "https://localhost:7190/api/Orders";

        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Order>?> Read()
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

            return JsonSerializer.Deserialize<List<Order>>(jsonString, options);
        }
        private async Task<bool> Create(Order curOrder)
        {
            var content = new StringContent(JsonSerializer.Serialize(curOrder),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> Update(Order curOrder)
        {
            var content = new StringContent(JsonSerializer.Serialize(curOrder),
            Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{URL}/{curOrder.Id}", content);
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
        private async Task<Order> GetOrderWithMaxId()
        {
            var response = await _httpClient.GetAsync($"{URL}/GetOrderWithMaxId");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                //משאירים כאן נקודת עצירה, לזיהוי שגיאה בשמירה

                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Deserialize<Order>(jsonString, options);
        }
        public async Task<bool> Create(Order curOrder, List<OrderProduct> curOrderProducts)
        {
            await Create(curOrder);
            Order maxIdOrder = await GetOrderWithMaxId();
            foreach (OrderProduct curOrderProduct in curOrderProducts)
            {
                curOrderProduct.Order = maxIdOrder;
            }
            var content = new StringContent(JsonSerializer.Serialize(curOrderProducts),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{URL}/OrderProducts", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                //משאירים כאן נקודת עצירה, לזיהוי שגיאות בשמירה.

                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<List<OrderProduct>?> ReadOrderProducts(Order curOrder)
        {
            var response = await _httpClient.GetAsync($"{URL}/OrderProducts/{curOrder.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Deserialize<List<OrderProduct>>(jsonString, options);
        }

        public async Task<bool> Update(Order curOrder, List<OrderProduct> curOrderProducts)
        {
            await Update(curOrder);

            var content = new StringContent(JsonSerializer.Serialize(curOrderProducts),
                     Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{URL}/OrderProducts", content);
            if (!response.IsSuccessStatusCode)
            {

                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }

    }
}


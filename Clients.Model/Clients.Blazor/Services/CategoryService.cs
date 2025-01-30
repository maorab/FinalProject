using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using Clients.Model;

namespace Clients.Blazor.Services
{
    public class CategoryService
    {
        const string URL = "https://localhost:7190/api/Category";

        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>?> Read()
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

            return JsonSerializer.Deserialize<List<Category>>(jsonString, options);
        }
        public async Task<bool> Create(Category curCategory)
        {
            var content = new StringContent(JsonSerializer.Serialize(curCategory),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Update(Category curCategory)
        {
            var content = new StringContent(JsonSerializer.Serialize(curCategory),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{URL}/{curCategory.Id}", content);
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
    }
}

using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using Clients.Model;

namespace Clients.Blazor.Services
{
    public class CompanyService
    {
        const string URL = "https://localhost:7190/api/Company";

        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Company>?> Read()
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

            return JsonSerializer.Deserialize<List<Company>>(jsonString, options);
        }
        public async Task<bool> Create(Company curCompany)
        {
            var content = new StringContent(JsonSerializer.Serialize(curCompany),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{response.StatusCode} - {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Update(Company curCompany)
        {
            var content = new StringContent(JsonSerializer.Serialize(curCompany),
            Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{URL}/{curCompany.Id}", content);
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

using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using Clients.Model;
namespace Clients.Blazor.Services
{
    public class ClientsService
    {
        private readonly HttpClient _httpClient;

        public ClientsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Client>?> Read()
        {
            // כתובת השירות אליו פונים
            string url = "https://localhost:5071/api/Client";
            // קבלת תשובה משירות זה ושמירה שלה 
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            // קריאה של התשובה כמחרוזת JSON, הוספת המרה מותאמת לרשימה של אובייקטים מסוג Country
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };

            return JsonSerializer.Deserialize<List<Client>>(jsonString, options);
        }
        const string URL = "https://localhost:5071/api/Client";//קבוע עם הכתובת של השירות
        public async Task<Client?> Read(int id)
        {
            var response = await _httpClient.GetAsync($"{URL}/{id}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
            return JsonSerializer.Deserialize<Client>(jsonString, options);
        }


    }
}

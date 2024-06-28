using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BunnySurfers.Blazor.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7274/");
        }

        public async Task<User?> GetUserById(int UserId)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/Users/{UserId}");
        }

        public class User
        {
            public int UserId { get; set; }
            public string ?Role { get; set; }
            public string ?Name { get; set; }
            public string ?Email { get; set; }
            public List<Course> Courses { get; set; } = new List<Course>();

           
        }
    }
}

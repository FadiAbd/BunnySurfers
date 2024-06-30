using BunnySurfers.API.Entities;


namespace BunnySurfers.Blazor.Services
{
    public class ModuleService : IModuleService
    {
        private readonly HttpClient _httpClient;

        public ModuleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateModule(Module module)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Module", module);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteModule(int moduleId)
        {
            var response = await _httpClient.DeleteAsync($"api/Module/{moduleId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Module> GetModuleById(int moduleId)
        {
            return await _httpClient.GetFromJsonAsync<Module>($"api/Module/{moduleId}");
        }

        public async Task<List<Module>> GetModules()
        {
            return await _httpClient.GetFromJsonAsync<List<Module>>("api/Module");
        }

        public async Task UpdateModule(int moduleId, Module moduleyew)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Module/{moduleId}", moduleyew);
            response.EnsureSuccessStatusCode();
        }
    }
}
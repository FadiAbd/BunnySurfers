using BunnySurfers.API.Entities;

namespace BunnySurfers.Blazor.Services
{
    public interface IModuleService
    {
        Task<List<Module>> GetModules();
        Task<Module> GetModuleById(int moduleId);
        Task CreateModule(Module module);
        Task UpdateModule(int moduleId, Module moduleyew);
        Task DeleteModule(int moduleId);
    }
}

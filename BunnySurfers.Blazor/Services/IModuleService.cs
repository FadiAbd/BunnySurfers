using BunnySurfers.API.Entities;

namespace BunnySurfers.Blazor.Services
{
    public interface IModuleService
    {
        Task CreateModule(Module module);
        Task DeleteModule(int moduleId);
        Task<Module> GetModuleById(int moduleId);
        Task<List<Module>> GetModules();
        Task UpdateModule(int moduleId, Module module);
    }
}
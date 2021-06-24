using Hogwarts.Domain.Interfaces.Service;
using Hogwarts.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hogwarts.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICharacterService, CharacterService>();
            
            
        }
    }
}

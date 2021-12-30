using OmenModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmenShips.Interfaces
{
    public interface IOmenTestRestService
    {
        Task<List<ShipModule>> GetShipModules();

        Task<List<StarshipClass>> GetStarshipClasses();

        Task<List<StarshipHull>> GetStarshipHulls();

        Task<List<Starship>> GetStarships();

        Task<bool> AddStarshipClass(StarshipClass model);

        Task<bool> AddStarshipHull(StarshipHull model);

        Task<bool> AddShipModule(ShipModule model);

        Task<Starship> AddOrUpdateStarship(Starship model);

        Task<bool> DeleteStarship(string id);
    }
}

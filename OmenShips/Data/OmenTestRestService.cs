using OmenModels;
using OmenShips.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OmenShips.Data
{
    public class OmenTestRestService : BaseRestService, IOmenTestRestService
    {

        private readonly string _baseRoute;
        private readonly string _starshipController = "Starship";

        public OmenTestRestService(HttpClient httpClient, Settings settings) : base(httpClient) 
        { 
            _baseRoute = settings.BaseApiRoute;
        }

        public async Task<List<ShipModule>> GetShipModules() => await GetRequestForListAsync<ShipModule>(_baseRoute, _starshipController, "ShipModuleList");

        public async Task<List<StarshipClass>> GetStarshipClasses() => await GetRequestForListAsync<StarshipClass>(_baseRoute, _starshipController, "StarshipClassList");

        public async Task<List<StarshipHull>> GetStarshipHulls() => await GetRequestForListAsync<StarshipHull>(_baseRoute, _starshipController, "StarshipHullList");

        public async Task<List<Starship>> GetStarships() => await GetRequestForListAsync<Starship>(_baseRoute, _starshipController, "StarshipList");

        public async Task<bool> AddStarshipClass(StarshipClass model)
        {
            HttpResponseMessage response = await PostRequestForResponseAsync(model, _baseRoute, _starshipController, "StarshipClass");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddStarshipHull(StarshipHull model)
        {
            HttpResponseMessage response = await PostRequestForResponseAsync(model, _baseRoute, _starshipController, "StarshipHull");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddOrReplaceShipModule(ShipModule model)
        {
            HttpResponseMessage response = await PostRequestForResponseAsync(model, _baseRoute, _starshipController, "ShipModule");

            return response.IsSuccessStatusCode;
        }

        public async Task<Starship> AddOrUpdateStarship(Starship model)
        {
            return await PostRequestForItemAsync(model, _baseRoute, _starshipController, "Starship");
        }

        public async Task<bool> DeleteStarship(string id)
        {
            HttpResponseMessage response = await PostRequestForResponseAsync(null, _baseRoute, _starshipController, $"Starship/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

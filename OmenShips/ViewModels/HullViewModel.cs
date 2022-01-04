using OmenModels;
using OmenShips.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class HullViewModel : BaseViewModel
    {
        private List<StarshipHull> _hulls = new List<StarshipHull>();
        public List<StarshipHull> Hulls
        {
            get => _hulls;
            set
            {
                SetValue(ref _hulls, value);
            }
        }

        private List<StarshipClass> _shipClasses = new List<StarshipClass>();
        public List<StarshipClass> ShipClasses
        {
            get => _shipClasses;
            set
            {
                SetValue(ref _shipClasses, value);
            }
        }

        private StarshipHull _newStarshipHull = new StarshipHull();
        public StarshipHull StarshipHull
        {
            get => _newStarshipHull;
            set
            {
                SetValue(ref _newStarshipHull, value);
            }
        }

        private readonly IOmenTestRestService _omenTestRestService;

        public HullViewModel(IOmenTestRestService omenTestRestService)
        {
            _omenTestRestService = omenTestRestService;
        }

        public async Task LoadViewModelAsync()
        {
            Hulls = await _omenTestRestService.GetStarshipHulls();
            ShipClasses = await _omenTestRestService.GetStarshipClasses();

            if (Hulls == null || !Hulls.Any())
            {
                Hulls = new List<StarshipHull>();
            }

            if(ShipClasses == null || !ShipClasses.Any())
            {
                ShipClasses = new List<StarshipClass>();
            }
        }

        public async Task AddNewShipHull()
        {
            _hulls.Add(StarshipHull);
            Hulls = _hulls;

            bool isSuccess = await _omenTestRestService.AddStarshipHull(StarshipHull);

            if (isSuccess)
            {
                StarshipHull = new StarshipHull();
            }
        }
    }
}

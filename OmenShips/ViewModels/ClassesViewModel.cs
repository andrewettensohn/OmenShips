using Newtonsoft.Json;
using OmenModels;
using OmenShips.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class ClassesViewModel : BaseViewModel
    {
        private List<StarshipClass> _shipClasses = new List<StarshipClass>();
        public List<StarshipClass> ShipClasses
        {
            get => _shipClasses;
            set
            {
                SetValue(ref _shipClasses, value);
            }
        }

        private StarshipClass _newShipClass = new StarshipClass();
        public StarshipClass NewShipClass
        {
            get => _newShipClass;
            set
            {
                SetValue(ref _newShipClass, value);
            }
        }

        private readonly IOmenTestRestService _omenTestRestService;

        public ClassesViewModel(IOmenTestRestService omenTestRestService)
        {
            _omenTestRestService = omenTestRestService;
        }

        public async Task LoadViewModelAsync()
        {
            ShipClasses = await _omenTestRestService.GetStarshipClasses();

            if(ShipClasses == null || !ShipClasses.Any())
            {
                ShipClasses = new List<StarshipClass>();
            }
        }

        public async Task AddNewShipClass()
        {
            _shipClasses.Add(NewShipClass);
            ShipClasses = _shipClasses;

            bool isSuccess = await _omenTestRestService.AddStarshipClass(NewShipClass);

            if(isSuccess)
            {
                NewShipClass = new StarshipClass();
            }
        }
    }
}

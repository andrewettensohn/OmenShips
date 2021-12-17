using Microsoft.AspNetCore.Components;
using OmenModels;
using OmenShips.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class FitsViewModel : BaseViewModel
    {
        private List<Starship> _ships = new List<Starship>();
        public List<Starship> Ships
        {
            get => _ships;
            set
            {
                SetValue(ref _ships, value);
            }
        }

        private List<StarshipClass> _classes = new List<StarshipClass>();
        public List<StarshipClass> Classes
        {
            get => _classes;
            set
            {
                SetValue(ref _classes, value);
            }
        }

        private StarshipClass _newShipSelectedClass;
        public StarshipClass NewShipSelectedClass
        {
            get => _newShipSelectedClass;
            set
            {
                SetValue(ref _newShipSelectedClass, value);
            }
        }

        private List<StarshipHull> _hulls = new List<StarshipHull>();
        public List<StarshipHull> Hulls
        {
            get => _hulls;
            set
            {
                SetValue(ref _hulls, value);
            }
        }

        private StarshipHull _newShipSelectedHull;
        public StarshipHull NewShipSelectedHull
        {
            get => _newShipSelectedHull;
            set
            {
                SetValue(ref _newShipSelectedHull, value);
            }
        }

        private List<ShipModule> _shipModules = new List<ShipModule>();
        public List<ShipModule> ShipModules
        {
            get => _shipModules;
            set
            {
                SetValue(ref _shipModules, value);
            }
        }

        private Starship _selectedShip;
        public Starship SelectedShip
        {
            get => _selectedShip;
            set
            {
                SetValue(ref _selectedShip, value);
            }
        }

        private Guid _selectedShipId;
        public Guid SelectedShipId
        {
            get => _selectedShipId;
            set
            {
                SetValue(ref _selectedShipId, value);
                SelectedShip = Ships.FirstOrDefault(x => x.Id == value);
            }
        }

        private Starship _newShip = new Starship();
        public Starship NewShip
        {
            get => _newShip;
            set
            {
                SetValue(ref _selectedShip, value);
            }
        }

        public Func<Starship, string> StarshipNameConverter = p => p?.Name;

        public Func<StarshipHull, string> HullNameConverter = p => p?.Name;

        public Func<StarshipClass, string> ClassNameConverter = p => p?.Name;

        public Func<ShipModule, string> ModuleNameConverter = p => p?.Name;

        private readonly IOmenTestRestService _omenTestRestService;

        public FitsViewModel(IOmenTestRestService omenTestRestService)
        {
            _omenTestRestService = omenTestRestService;
        }

        public async Task LoadViewModelAsync()
        {
            Ships = await _omenTestRestService.GetStarships();
            Hulls = await _omenTestRestService.GetStarshipHulls();
            Classes = await _omenTestRestService.GetStarshipClasses();
            ShipModules = await _omenTestRestService.GetShipModules();

            if (Ships == null || !Ships.Any())
            {
                Ships = new List<Starship>();
            }

            SelectedShip = Ships.FirstOrDefault();
            NewShipSelectedClass = Classes.FirstOrDefault();
            NewShipSelectedHull = Hulls.FirstOrDefault();
        }

        public async Task AddNewShip()
        {
            NewShip.Hull = NewShipSelectedHull;
            NewShip.StarshipClass = NewShipSelectedClass;
            NewShip.Modules = new List<ShipModule>();

            ShipModule emptyModule = ShipModules.FirstOrDefault(x => x.Category == ModuleCategory.EmptySlot);

            if(emptyModule != null)
            {
                for (int i = 0; i < NewShip.StarshipClass.Slots; i++)
                {
                    NewShip.Modules.Add(emptyModule);
                }
            }

            bool isSuccess = await _omenTestRestService.AddStarship(NewShip);

            if(isSuccess)
            {
                Ships = await _omenTestRestService.GetStarships();
                SelectedShip = NewShip;
            }
        }

        public void AddModuleToShip(ChangeEventArgs args)
        {
            bool isGuid = Guid.TryParse(args.Value.ToString(), out Guid newModuleId);

            if (!isGuid) return;

            ShipModule newModule = ShipModules.FirstOrDefault(x => x.Id == newModuleId);
            
            if(newModule != null)
            {
                SelectedShip.Modules.Add(newModule);
                SelectedShip = SelectedShip;
            }
        }
    }
}

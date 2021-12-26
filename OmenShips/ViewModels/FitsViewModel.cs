﻿using Microsoft.AspNetCore.Components;
using MongoDB.Bson;
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
                GetModuleSlotViewModelsForSelectedShip();
                StarshipStatsViewModel = new StarshipStatsViewModel(value);
            }
        }

        public StarshipStatsViewModel _starshipStatsViewModel;
        public StarshipStatsViewModel StarshipStatsViewModel
        {
            get => _starshipStatsViewModel;
            set
            {
                SetValue(ref _starshipStatsViewModel, value);
            }
        }

        public List<ModuleSlotViewModel> _moduleSlotViewModels;
        public List<ModuleSlotViewModel> ModuleSlotViewModels
        {
            get => _moduleSlotViewModels;
            set
            {
                SetValue(ref _moduleSlotViewModels, value);
            }
        }

        private string _newShipName;
        public string NewShipName
        {
            get => _newShipName;
            set
            {
                SetValue(ref _newShipName, value);
            }
        }

        private readonly IOmenTestRestService _omenTestRestService;

        private ShipModule _emptyModule;

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

            _emptyModule = ShipModules.FirstOrDefault(x => x.Category == ModuleCategory.EmptySlot);

            if (Ships == null || !Ships.Any())
            {
                Ships = new List<Starship>();
            }

            SelectedShip = Ships.FirstOrDefault();
            NewShipSelectedClass = Classes.FirstOrDefault();
            NewShipSelectedHull = Hulls.FirstOrDefault();

            if(SelectedShip != null)
            {
                GetModuleSlotViewModelsForSelectedShip();
            }
        }

        public async Task AddNewShip()
        {
            Starship submittedShip = new Starship
            {
                Name = NewShipName,
                Hull = NewShipSelectedHull,
                StarshipClass = NewShipSelectedClass,
                Modules = new List<ShipModule>()
            };

            if(_emptyModule != null)
            {
                for (int i = 0; i < submittedShip.StarshipClass.Slots; i++)
                {
                    submittedShip.Modules.Add(_emptyModule);
                }
            }

            submittedShip = await _omenTestRestService.AddOrUpdateStarship(submittedShip);

            if(!string.IsNullOrEmpty(submittedShip.Id))
            {
                Ships = await _omenTestRestService.GetStarships();
                SelectedShip = submittedShip;
                GetModuleSlotViewModelsForSelectedShip();
            }
        }

        public async Task AddModuleToShip(ShipModule newModule)
        {
            int availableSlots = SelectedShip.Modules.Count(x => x.Category == ModuleCategory.EmptySlot);

            if(newModule.SlotSpacesRequired > availableSlots) return;

            int emptyModulesFilled = 0;
            for(int i = 0; i < SelectedShip.Modules.Count; i++)
            {
                if(emptyModulesFilled < newModule.SlotSpacesRequired && SelectedShip.Modules[i].Category == ModuleCategory.EmptySlot)
                {
                    SelectedShip.Modules.RemoveAt(i);
                    emptyModulesFilled++;
                }
            }

            SelectedShip.Modules.Add(newModule);

            SetSelectedShip();

            await _omenTestRestService.AddOrUpdateStarship(SelectedShip);
        }

        public async Task UninstallModule(ShipModule moduleToUninstall)
        {
            SelectedShip.Modules.Remove(moduleToUninstall);

            for(int i = 0; i < moduleToUninstall.SlotSpacesRequired; i++)
            {
                SelectedShip.Modules.Add(_emptyModule);
            }

            SetSelectedShip();

            await _omenTestRestService.AddOrUpdateStarship(SelectedShip);
        }

        private void SetSelectedShip()
        {
            OnPropertyChanged(nameof(SelectedShip));
            GetModuleSlotViewModelsForSelectedShip();
            StarshipStatsViewModel = new StarshipStatsViewModel(SelectedShip);
        }

        private void GetModuleSlotViewModelsForSelectedShip()
        {
            ModuleSlotViewModels = new List<ModuleSlotViewModel>();
            SelectedShip.Modules.ForEach(x => ModuleSlotViewModels.Add(new ModuleSlotViewModel(this, x)));
            OnPropertyChanged(nameof(ModuleSlotViewModels));
        }
    }
}

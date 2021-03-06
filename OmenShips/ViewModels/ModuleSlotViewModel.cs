using Microsoft.AspNetCore.Components;
using MudBlazor;
using OmenModels;
using OmenShips.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class ModuleSlotViewModel : BaseViewModel
    {

        private ShipModule _shipModule;
        public ShipModule ShipModule
        {
            get => _shipModule;
            set
            {
                SetValue(ref _shipModule, value);
            }
        }

        private List<ShipModule> _shipModuleList;
        public List<ShipModule> ShipModuleList
        {
            get => _shipModuleList;
            set
            {
                SetValue(ref _shipModuleList, value);
            }
        }

        private bool _isEmptyModule;
        public bool IsEmptyModule
        {
            get => _isEmptyModule;
            set
            {
                SetValue(ref _isEmptyModule, value);
            }
        }

        private string _moduleSelectionSearch;
        public string ModuleSelectionSearch
        {
            get => _moduleSelectionSearch;
            set
            {
                SetValue(ref _moduleSelectionSearch, value);
            }
        }

        public ShipModulesViewModel _shipModulesViewModel;
        public ShipModulesViewModel ShipModulesViewModel
        {
            get => _shipModulesViewModel;
            set
            {
                SetValue(ref _shipModulesViewModel, value);
            }
        }

        private readonly FitsViewModel _parentViewModel;

        public ModuleSlotViewModel(FitsViewModel fitsViewModel, ShipModule module)
        {
            _parentViewModel = fitsViewModel;
            _shipModule = module;
            _shipModuleList = _parentViewModel.ShipModules;
            _isEmptyModule = module.Category == ModuleCategory.EmptySlot;
            _shipModulesViewModel = new ShipModulesViewModel();

            ShipModulesViewModel.LoadViewModelForFitting(this);
        }

        public async Task OnModuleInstalled(ShipModule shipModule)
        {
            await _parentViewModel.AddModuleToShip(shipModule);
        }

        public async Task OnModuleUninstall()
        {
            if (!IsEmptyModule)
            {
                await _parentViewModel.UninstallModule(ShipModule);
            }
        }
    }
}
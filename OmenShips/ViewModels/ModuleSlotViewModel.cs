using Microsoft.AspNetCore.Components;
using OmenModels;
using System.Collections.Generic;
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

        private readonly FitsViewModel _parentViewModel;

        public ModuleSlotViewModel(FitsViewModel fitsViewModel, ShipModule module)
        {
            _parentViewModel = fitsViewModel;
            _shipModule = module;
            _shipModuleList = _parentViewModel.ShipModules;
            _isEmptyModule = module.Category == ModuleCategory.EmptySlot;
        }

        public async Task OnModuleSelectedConfirmation()
        {
            IsEmptyModule = ShipModule.Category == ModuleCategory.EmptySlot;

            if(!IsEmptyModule)
            {
                await _parentViewModel.AddModuleToShip(ShipModule);
            }
        }

        public async Task OnModuleUninstall()
        {
            if(!IsEmptyModule)
            {
                await _parentViewModel.UninstallModule(ShipModule);
            }
        }
    }
}
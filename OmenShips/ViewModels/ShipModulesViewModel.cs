using OmenModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class ShipModulesViewModel : BaseViewModel
    {

        private List<ShipModule> _modules = new List<ShipModule>();
        public List<ShipModule> Modules
        {
            get => _modules;
            set
            {
                SetValue(ref _modules, value);
            }
        }

        private string _moduleSearchString;
        public string ModuleSearchString
        {
            get => _moduleSearchString;
            set
            {
                SetValue(ref _moduleSearchString, value);
            }
        }

        public bool IsModuleSlotSelection { get; set; }

        private ModuleSlotViewModel _slotViewModel;

        public bool ModuleSelectionFilterFunc(ShipModule module)
        {
            if (string.IsNullOrWhiteSpace(ModuleSearchString)) return true;

            if (module.Name.Contains(ModuleSearchString, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public async Task OnModuleInstalled(ShipModule module) => await _slotViewModel.OnModuleInstalled(module);

        public void LoadViewModelForFitting(ModuleSlotViewModel moduleSlotViewModel)
        {
            IsModuleSlotSelection = true;
            Modules = moduleSlotViewModel.ShipModuleList;
            _slotViewModel = moduleSlotViewModel;
        }

        public void LoadViewModelForShipModuleList(ModuleAddEditViewModel shipModulesViewModel)
        {
            Modules = shipModulesViewModel.Modules;
        }
    }
}

using OmenModels;
using OmenShips.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class ModuleAddEditViewModel : BaseViewModel
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

        private ShipModule _selectedModule = new ShipModule();
        public ShipModule SelectedModule
        {
            get => _selectedModule;
            set
            {
                SetValue(ref _selectedModule, value);
            }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                SetValue(ref _isEditMode, value);
                if(value)
                {
                    SelectedModule = _modules.FirstOrDefault();
                }
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

        private readonly IOmenTestRestService _omenTestRestService;

        public ModuleAddEditViewModel(IOmenTestRestService omenTestRestService)
        {
            _omenTestRestService = omenTestRestService;
            _shipModulesViewModel = new ShipModulesViewModel();
        }

        public async Task LoadViewModelAsync()
        {
            Modules = await _omenTestRestService.GetShipModules();

            if (Modules == null || !Modules.Any())
            {
                Modules = new List<ShipModule>();
            }
            else
            {
                Modules.OrderByDescending(x => x.Name);
                ShipModulesViewModel.LoadViewModelForShipModuleList(this);
            }
        }

        public async Task AddOrReplaceModule()
        {

            bool isSuccess = await _omenTestRestService.AddOrReplaceShipModule(SelectedModule);

            if (isSuccess)
            {
                Modules = await _omenTestRestService.GetShipModules();
                ShipModulesViewModel.LoadViewModelForShipModuleList(this);
            }
        }
    }
}

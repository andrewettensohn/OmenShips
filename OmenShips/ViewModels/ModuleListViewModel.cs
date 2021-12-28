using OmenModels;
using OmenShips.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class ModuleListViewModel : BaseViewModel
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

        private ShipModule _newStarshipModule = new ShipModule();
        public ShipModule NewStarshipModule
        {
            get => _newStarshipModule;
            set
            {
                SetValue(ref _newStarshipModule, value);
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

        public ModuleListViewModel(IOmenTestRestService omenTestRestService)
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

            ShipModulesViewModel.LoadViewModelForShipModuleList(this);
        }

        public async Task AddNewShipModule()
        {
            _modules.Add(NewStarshipModule);
            Modules = _modules;

            bool isSuccess = await _omenTestRestService.AddShipModule(NewStarshipModule);

            if (isSuccess)
            {
                NewStarshipModule = new ShipModule();
            }
        }
    }
}

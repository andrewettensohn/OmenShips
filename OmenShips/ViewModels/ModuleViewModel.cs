using OmenModels;
using OmenShips.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmenShips.ViewModels
{
    public class ModuleViewModel : BaseViewModel
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

        private readonly IOmenTestRestService _omenTestRestService;

        public ModuleViewModel(IOmenTestRestService omenTestRestService)
        {
            _omenTestRestService = omenTestRestService;
        }

        public async Task LoadViewModelAsync()
        {
            Modules = await _omenTestRestService.GetShipModules();

            if (Modules == null || !Modules.Any())
            {
                Modules = new List<ShipModule>();
            }
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

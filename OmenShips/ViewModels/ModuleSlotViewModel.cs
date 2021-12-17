using OmenModels;

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

        private bool _isEmptyModule;
        public bool IsEmptyModule
        {
            get => _isEmptyModule;
            set
            {
                SetValue(ref _isEmptyModule, value);
            }
        }

        public FitsViewModel ParentViewModel;

        public ModuleSlotViewModel(FitsViewModel fitsViewModel, ShipModule module)
        {
            ParentViewModel = fitsViewModel;
            _shipModule = module;
            _isEmptyModule = module.Category == ModuleCategory.EmptySlot;
        }
    }
}
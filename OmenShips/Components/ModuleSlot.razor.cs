using Microsoft.AspNetCore.Components;
using OmenModels;
using OmenShips.ViewModels;
using System.Threading.Tasks;

namespace OmenShips.Components
{
    public partial class ModuleSlot
    {

        [CascadingParameter]
        public FitsViewModel ParentViewModel { get; set; }

        [Parameter]
        public ShipModule ShipModule { get; set; }

        private ModuleSlotViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ModuleSlotViewModel(ParentViewModel, ShipModule);

            _vm.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}

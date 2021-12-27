using Microsoft.AspNetCore.Components;
using MudBlazor;
using OmenModels;
using OmenShips.ViewModels;
using System.Threading.Tasks;

namespace OmenShips.Components
{
    public partial class ModuleSlot
    {

        [Parameter]
        public ModuleSlotViewModel ViewModel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}

using Microsoft.AspNetCore.Components;
using OmenShips.ViewModels;
using System.Threading.Tasks;

namespace OmenShips.Components
{
    public partial class ShipModules
    {
        [Parameter]
        public ShipModulesViewModel ViewModel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}

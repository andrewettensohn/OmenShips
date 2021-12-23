using Microsoft.AspNetCore.Components;
using OmenModels;
using OmenShips.Data;
using OmenShips.ViewModels;
using System.Threading.Tasks;

namespace OmenShips.Components
{
    public partial class StarshipStats
    {
        [Parameter]
        public StarshipStatsViewModel ViewModel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}

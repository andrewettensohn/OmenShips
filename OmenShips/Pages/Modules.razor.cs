using Microsoft.AspNetCore.Components;
using MudBlazor;
using OmenShips.Interfaces;
using OmenShips.ViewModels;
using System.Threading.Tasks;

namespace OmenShips.Pages
{
    public partial class Modules
    {
        [Inject]
        public IOmenTestRestService OmenTestRestService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        private ModuleAddEditViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ModuleAddEditViewModel(OmenTestRestService, Snackbar);
            await _vm.LoadViewModelAsync();

            _vm.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using OmenShips.Interfaces;
using OmenShips.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace OmenShips.Pages
{
    public partial class Classes
    {
        [Inject]
        public IOmenTestRestService OmenTestRestService { get; set; }

        private ClassesViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ClassesViewModel(OmenTestRestService);
            await _vm.LoadViewModelAsync();

            _vm.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}

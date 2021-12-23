﻿using Microsoft.AspNetCore.Components;
using OmenShips.Interfaces;
using OmenShips.ViewModels;
using System.Threading.Tasks;

namespace OmenShips.Pages
{
    public partial class Modules
    {
        [Inject]
        public IOmenTestRestService OmenTestRestService { get; set; }

        private ModuleViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ModuleViewModel(OmenTestRestService);
            await _vm.LoadViewModelAsync();

            _vm.PropertyChanged += (sender, e) => StateHasChanged();

            await base.OnInitializedAsync();
        }
    }
}
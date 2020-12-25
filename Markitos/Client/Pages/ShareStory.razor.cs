using Markitos.Client.Services;
using Markitos.Shared.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Markitos.Client.Pages
{
    public class ShareStoryBase : ComponentBase
    {
        protected List<ReadStoryModel> _stories;
        protected bool _isloading;
        [Inject] BackendService _httpService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] protected IMatToaster _toaster { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _isloading = true;
            APIResultModel result = await _httpService.CallGetApiAsync(_navigationManager.BaseUri +
                "api/Story/getStories");
            if (result.Success)
            {
                _stories = JsonSerializer.Deserialize<List<ReadStoryModel>>(result.Message);
            }
            else
            {
                _toaster.Add(result.Message, MatToastType.Danger);
            }
            _isloading = false;
        }

    }
}

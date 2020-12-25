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
        protected StoryModel _story= new();
        [Inject] BackendService _httpService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] protected IMatToaster _toaster { get; set; }

        protected async Task SubmitStoryAsync()
        {
            if(string.IsNullOrWhiteSpace(_story.Story))
            {
                return;
            }
            else
            {
                APIResultModel result = await _httpService.PostToBackend(_navigationManager.BaseUri 
                    + "api/Story/share", JsonSerializer.Serialize(_story));
                if(result.Success)
                {
                    _toaster.Add(result.Message, MatToastType.Success);
                }
                else
                {
                    _toaster.Add(result.Message, MatToastType.Danger);
                }
            }
        }

    }
}

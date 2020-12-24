using Markitos.Client.Services;
using Markitos.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
            }
        }

    }
}

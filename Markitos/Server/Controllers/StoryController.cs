using Markitos.Server.Manager;
using Markitos.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Markitos.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        [HttpPost]
        [Route("share")]
        public string ShareStoryAsync(StoryModel story)
        {
            if(story == null || string.IsNullOrWhiteSpace(story.Story))
            {
                return "Error: Story cannot be empty";
            }
            ClaimsPrincipal user = User;
            string name = ClaimsManager.GetUserFirstName(User);
            string Lastname = ClaimsManager.GetUserLastName(User);
            return "";
        }
    }
}

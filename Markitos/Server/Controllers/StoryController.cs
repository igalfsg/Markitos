using Markitos.Server.Manager;
using Markitos.Server.Models;
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
        private readonly StoryManagercs _storyManager;
        public StoryController(StoryManagercs storyManagercs)
        {
            _storyManager = storyManagercs;
        }
        [HttpPost]
        [Route("share")]
        public async Task<string> ShareStoryAsync(StoryModel story)
        {
            if(story == null || string.IsNullOrWhiteSpace(story.Story))
            {
                return "Error: Story cannot be empty";
            }
            string name = ClaimsManager.GetUserFirstName(User);
            string Lastname = ClaimsManager.GetUserLastName(User);
            DBStoryModel dbStory = new DBStoryModel(story, name + " " + Lastname);
            return await _storyManager.AddStoryAsync(dbStory);
        }

        [HttpGet]
        [Route("getStories")]
        public List<ReadStoryModel> GetStories()
        {
            return _storyManager.GetStories();
        }
    }
}

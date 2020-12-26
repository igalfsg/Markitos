using Markitos.Server.Manager;
using Markitos.Server.Models;
using Markitos.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            string userID = ClaimsManager.GetUserObjectID(User);
            if(userID.Equals("36e8e535-23b8-4af1-b29e-544ea846e674") 
                || userID.Equals("dc4b9a59-f6e9-4ecb-b02f-3319a726bebb"))
            {
                return _storyManager.Admin();
            }
            else
            {
                return _storyManager.GetStories();
            }
        }
    }
}

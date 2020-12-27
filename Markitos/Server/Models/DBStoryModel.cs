using Markitos.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Markitos.Server.Models
{
    public class DBStoryModel
    {
        public DBStoryModel()
        {

        }
        public DBStoryModel(StoryModel story, string name)
        {
            Story = story.Story;
            ShareAnon = story.ShareAnon;
            ShareWithFamOnly = story.ShareWithFamOnly;
            Name = name;
        }
        [Key]
        public string PostID { get; set; } = Guid.NewGuid().ToString();
        public string Story { get; set; }
        public string Name { get; set; }
        public bool ShareWithFamOnly { get; set; } = false;
        public bool ShareAnon { get; set; } = false;
        public DateTime TimeSubmited { get; set; } = DateTime.UtcNow;
    }
}

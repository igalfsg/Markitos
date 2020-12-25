using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Markitos.Shared.Models
{
    public class ReadStoryModel
    {
        public ReadStoryModel()
        {

        }
        public ReadStoryModel (string story, string name)
        {
            Story = story;
            Author = name;
        }
        [JsonPropertyName("Story")]
        public string Story { get; set; }
        [JsonPropertyName("Author")]
        public string Author { get; set; }
    }
}

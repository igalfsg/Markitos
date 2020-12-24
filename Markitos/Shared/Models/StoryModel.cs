using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Markitos.Shared.Models
{
    public class StoryModel
    {
        [JsonPropertyName("Story")]
        public string Story { get; set; }
        [JsonPropertyName("ShareWithFamOnly")]
        public bool ShareWithFamOnly { get; set; } = false;
        [JsonPropertyName("ShareAnon")]
        public bool ShareAnon { get; set; } = false;
    }
}

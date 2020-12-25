using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Markitos.Shared.Models
{
    public class UploadedFile
    {
        [JsonPropertyName("FileName")]
        public string FileName { get; set; }
        [JsonPropertyName("FileContent")]
        public byte[] FileContent { get; set; }
    }
}

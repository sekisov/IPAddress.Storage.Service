using System.Text.Json.Serialization;

namespace IPAddress.Storage.Service.Domain.Models.Abstract
{
    public abstract class EditableEntityDTO : EntityDTO
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; set; } 
    }
}

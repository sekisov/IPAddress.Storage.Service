
using IPAddress.Storage.Service.DataAccess.Models.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddress.Storage.Service.DataAccess.Models
{
    public class EditableEntity : Entity, IEditableEntity
    {
        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("CREATED_BY")]
        public string? CreatedBy { get; set; }

        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; }

        [Column("UPDATED_BY")]
        public string? UpdatedBy { get; set; }
    }
}

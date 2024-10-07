using System.ComponentModel.DataAnnotations;

namespace IPAddress.Storage.Service.Domain.Models.Abstract
{
    public abstract class EntityDTO
    {
        [Key]
        public virtual long Id { get; set; } 
    }
}

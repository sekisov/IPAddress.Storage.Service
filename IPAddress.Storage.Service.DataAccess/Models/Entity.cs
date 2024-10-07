using IPAddress.Storage.Service.DataAccess.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddress.Storage.Service.DataAccess.Models
{
    public class Entity : IEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
    }
}

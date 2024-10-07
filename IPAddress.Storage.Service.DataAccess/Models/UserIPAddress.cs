using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddress.Storage.Service.DataAccess.Models
{
    [Table("USER_IP_ADRESSES")]
    public class UserIPAddress : EditableEntity
    {
        [Column("ADDRESS")]
        public string? Address { get; set; }

        [Column("LAST_CONNECTION")]
        public DateTime LastConnection { get; set; }


        public ICollection<User> Users { get; set; }

        public UserIPAddress()
        {
            Users = new List<User>();
        }
    }
}

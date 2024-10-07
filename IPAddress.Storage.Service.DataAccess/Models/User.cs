using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddress.Storage.Service.DataAccess.Models
{
    [Table("USERS")]
    public class User: EditableEntity
    {
        [Column("Username")]

        public string? Username { get; set; }

        public ICollection<UserIPAddress> UserIPAddresses { get; set; }
        public User()
        {
            UserIPAddresses = new List<UserIPAddress>();
        }
    }
}

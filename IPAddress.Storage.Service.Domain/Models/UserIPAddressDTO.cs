using IPAddress.Storage.Service.Domain.Models.Abstract;

namespace IPAddress.Storage.Service.Domain.Models
{
    public class UserIPAddressDTO : EditableEntityDTO
    {
        public string? Address { get; set; }

        public DateTime LastConnection { get; set; }

        public ICollection<UserDTO>? Users { get; set; } = new List<UserDTO>();
    }
}

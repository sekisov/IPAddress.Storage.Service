using IPAddress.Storage.Service.Domain.Models.Abstract;

namespace IPAddress.Storage.Service.Domain.Models
{
    public class UserDTO : EditableEntityDTO
    {
        public string? Username { get; set; }

        public ICollection<UserIPAddressDTO>? UserIPAddresses { get; set; } = new List<UserIPAddressDTO>();
    }
}

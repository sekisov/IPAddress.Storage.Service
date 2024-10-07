namespace IPAddress.Storage.Service.Models
{
    public class UserIPAddressRequest
    {
        public string? IPAddress { get; set; }
        public long? UserId { get; set; }
        public string? Username { get; set; }
    }
}

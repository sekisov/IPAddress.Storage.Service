namespace IPAddress.Storage.Service.DataAccess.Models.Abstract
{
    public interface IEditableEntity : IEntity
    {
        DateTime CreatedAt { get;set; }
        string? CreatedBy { get;set; }
        DateTime UpdatedAt { get;set; }
        string? UpdatedBy { get; set; }

    }
}

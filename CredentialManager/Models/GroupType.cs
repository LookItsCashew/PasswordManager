using SQLite;

namespace CredentialManager.Models;

[Table("GroupType")]
public class GroupType
{
    public int GroupTypeId { get; set; }
    public string GroupTypeName { get; set; }
    public string GroupTypeDescription { get; set; }
}
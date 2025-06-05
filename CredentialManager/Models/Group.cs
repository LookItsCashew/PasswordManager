using SQLite;

namespace CredentialManager.Models;

[Table("Group")]
public class Group
{
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public string GroupTypeID { get; set; }
}
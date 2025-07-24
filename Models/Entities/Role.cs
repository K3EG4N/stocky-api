namespace Models.Entities;

public class Role
{
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}

public class ROLE_ENUM
{
    public const string
        USER = "01";
}
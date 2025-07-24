namespace Models.DTO;

public class ListUserDto
{
    public string? UserName { get; set; }
    public List<string> Roles { get; set; } = [];
}
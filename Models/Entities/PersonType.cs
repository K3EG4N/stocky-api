namespace Models.Entities;

public class PersonType
{
    public Guid  PersonTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; 
}
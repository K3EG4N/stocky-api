namespace Models.Entities;

public class Person
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid PersonTypeId { get; set; }
    public PersonType Type { get; set; } = new PersonType();
    public bool Active { get; set; }
}
// DTO za kreiranje korisnika
//
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Customer";
    public DateTime CreatedAt { get; set; }
}


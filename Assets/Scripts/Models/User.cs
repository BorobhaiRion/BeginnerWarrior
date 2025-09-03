using SQLite;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }
}

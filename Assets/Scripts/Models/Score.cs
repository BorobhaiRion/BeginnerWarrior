using SQLite;

public class Score
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Email { get; set; }

    public int Coins { get; set; }
}

using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class AdminUserInfo : MonoBehaviour
{
    public Text outputText;

    void Start()
    {
        var users = DatabaseManager.db.Table<User>();
        var sb = new StringBuilder();
        foreach (var u in users.Where(u => !u.IsAdmin))
            sb.AppendLine($"Email: {u.Email} | Pass: {u.Password}");
        outputText.text = sb.ToString();
    }
}

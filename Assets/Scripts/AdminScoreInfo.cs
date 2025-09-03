using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;

public class AdminScoreInfo : MonoBehaviour
{
    public Text outputText;

    void Start()
    {
        var scores = DatabaseManager.db.Table<Score>()
                         .OrderByDescending(s => s.Coins);
        var sb = new StringBuilder();
        foreach (var s in scores)
            sb.AppendLine($"Email: {s.Email} || Coins: {s.Coins}");
        outputText.text = sb.ToString();
    }
}

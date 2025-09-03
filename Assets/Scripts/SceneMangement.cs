using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadLevel()
    {
        string email = PlayerPrefs.GetString("email", "unknown@example.com");

        // Get the real currentCoin from Player script
        int coinsCollected = Player.currentCoin;

        DatabaseManager.db.Insert(new Score
        {
            Email = email,
            Coins = coinsCollected
        });

        SceneManager.LoadScene("victoryPanel");
    }

    public void PlayGame()
    {
        Player.currentCoin = 0; // Reset when game starts
        SceneManager.LoadScene("level1");
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathScript : MonoBehaviour
{
    public Button playAgainButton;
    public Button exitButton;
    public TextMeshProUGUI feedbackText;
    // Add this for Exit functionality

    void Start()
    {
        playAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Level1");
        });

        exitButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }
}


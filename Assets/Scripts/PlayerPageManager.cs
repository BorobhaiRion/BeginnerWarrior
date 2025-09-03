using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPageManager : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    // Add this for Exit functionality

    void Start()
    {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Level1");
        });

        exitButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }
}


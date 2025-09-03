using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminGameManager : MonoBehaviour
{

    public Button exitButton; // Add this for Exit functionality


    void Start()
    {
       
        exitButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }
}

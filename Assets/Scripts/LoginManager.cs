using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public InputField emailField;
    public InputField passField;
    public TextMeshProUGUI feedbackText;

    public void OnLogin()
    {
        string email = emailField.text;
        string pw = passField.text;

        var user = DatabaseManager.db.Table<User>()
                    .Where(u => u.Email == email)
                    .FirstOrDefault();

        if (user == null)
        {
            feedbackText.text = "Invalid email.";
            return;
        }

        if (user.Password != pw)
        {
            feedbackText.text = "Password is incorrect.";
            return;
        }

        PlayerPrefs.SetString("email", email);
        PlayerPrefs.SetInt("isAdmin", user.IsAdmin ? 1 : 0);

        if (user.IsAdmin)
            SceneManager.LoadScene("AdminPanel");
        else
            SceneManager.LoadScene("PlayerPage");
    }

    public void OnGoToSignup() =>
        SceneManager.LoadScene("SignupScene");

    // ? NEW: Exit button handler
    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

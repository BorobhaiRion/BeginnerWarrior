using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using System.Text.RegularExpressions;

public class SignupManager : MonoBehaviour
{
    public InputField emailField;
    public InputField passField;
    public TMP_Text feedbackText;
    public Button exitButton; // Add this for Exit functionality

    public void OnSignup()
    {
        string email = emailField.text;
        string pw = passField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pw))
        {
            feedbackText.text = "Fill in both fields.";
            return;
        }

        if (!IsValidEmail(email))
        {
            feedbackText.text = "Invalid email format. Must contain @ and .com";
            return;
        }

        var exists = DatabaseManager.db.Table<User>()
                      .Where(u => u.Email == email)
                      .Any();

        if (exists)
        {
            feedbackText.text = "Email already registered.";
            return;
        }

        DatabaseManager.db.Insert(new User
        {
            Email = email,
            Password = pw,
            IsAdmin = false
        });

        SceneManager.LoadScene("MainMenu");
    }

    private bool IsValidEmail(string email)
    {
        // Basic email pattern requiring @ and ending with .com
        string pattern = @"^[^@\s]+@[^@\s]+\.com$";
        return Regex.IsMatch(email, pattern);
    }

    void Start()
    {

        exitButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }
}

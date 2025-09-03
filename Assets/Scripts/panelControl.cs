using UnityEngine;

public class StartupPanelController : MonoBehaviour
{
    public GameObject panel;
    public float delay = 2f;

    void Start()
    {
        Invoke("HidePanel", delay);
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }
}

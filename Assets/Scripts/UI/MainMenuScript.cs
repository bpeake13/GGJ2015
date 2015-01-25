using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        showOnLoad.alpha = 1;
        hideOnLoad.alpha = 0;
        Application.LoadLevel(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    [SerializeField]
    private CanvasGroup showOnLoad;

    [SerializeField]
    private CanvasGroup hideOnLoad;
}

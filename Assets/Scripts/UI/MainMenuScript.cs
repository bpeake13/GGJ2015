using UnityEngine;
using System.Collections;
using System.IO;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        showOnLoad.alpha = 1;
        hideOnLoad.alpha = 0;
        Application.LoadLevel(1);
    }

    public void StartReplay()
    {
        if (!File.Exists("recordings/rec_000.bin"))
            return;

        showOnLoad.alpha = 1;
        hideOnLoad.alpha = 0;
        Application.LoadLevel(2);
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverMenuScript : MonoBehaviour
{
    void Start()
    {
        WinnerDataStructure wds = WinnerDataStructure.Instance;
        winnerText.text = wds.WinnerString;
    }

    public void Exit()
    {
        Application.Quit();
    }

    [SerializeField]
    private Text winnerText;
}

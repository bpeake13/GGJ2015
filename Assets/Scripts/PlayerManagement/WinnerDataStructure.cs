using UnityEngine;
using System.Collections;

public class WinnerDataStructure : MonoBehaviour
{

    public static WinnerDataStructure Create(int winnerIndex)
    {
        if(instance)
        {
            instance.winnerIndex = winnerIndex;
            return instance;
        }

        GameObject go = new GameObject("WDS", typeof(WinnerDataStructure));
        WinnerDataStructure wds = go.GetComponent<WinnerDataStructure>();
        wds.winnerIndex = winnerIndex;

        return wds;
    }

    public int WinnerIndex
    {
        get { return winnerIndex; }
        set { winnerIndex = value; }
    }

    public string WinnerString
    {
        get
        {
            return string.Format("Player {0}\nwins!", winnerIndex + 1);
        }
    }

    public static WinnerDataStructure Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private int winnerIndex;

    private static WinnerDataStructure instance;
}

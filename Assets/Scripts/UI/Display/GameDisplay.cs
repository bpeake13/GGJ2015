using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{
    public static GameDisplay Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameplayStatistics gs = GameplayStatistics.Instance;

        int i = 0;
        foreach(PlayerController player in gs.IteratePlayers())
        {
            healthDisplays[i].BindPlayer(player);
            i++;
        }
    }

    public void SetPlayerTurn(PlayerController player)
    {
        messageText.text = string.Format("P{0}\nTURN", player.Index + 1);
    }

    public void HideIndicator(PlayerController player)
    {
        Indicator indicator = GetIndicator(player);

        indicator.gameObject.SetActive(false);
    }

    public void ShowAction(PlayerController player)
    {
        Indicator indicator = GetIndicator(player);
        indicator.SetPlayer(player);

        indicator.gameObject.SetActive(true);
        indicator.ShowActionItem();
    }

    public void ShowReaction(PlayerController player)
    {
        Indicator indicator = GetIndicator(player);
        indicator.SetPlayer(player);

        indicator.gameObject.SetActive(true);
        indicator.ShowReactionItem();
    }

    public void ShowStunned(PlayerController player)
    {
        Indicator indicator = GetIndicator(player);
        indicator.SetPlayer(player);

        indicator.gameObject.SetActive(true);
        indicator.ShowStunnedItem();
    }

    public void ShowSlowed(PlayerController player)
    {
        Indicator indicator = GetIndicator(player);
        indicator.SetPlayer(player);

        indicator.gameObject.SetActive(true);
        indicator.ShowSlowedItem();
    }

    public void SetTime(float time)
    {
        GameplayStatistics gs = GameplayStatistics.Instance;
        float max = gs.MaxReactionTime;

        float timeLeft = max - time;

        timeText.text = string.Format("{0:0.0}s", timeLeft);
        

        float normalized = timeLeft / max;
        timeSlider.normalizedValue = normalized;
    }

    public void ShowTimer()
    {
        timeSlider.gameObject.SetActive(true);
    }

    public void HideTimer()
    {
        timeSlider.gameObject.SetActive(false);
    }

    private Indicator GetIndicator(PlayerController player)
    {
        return indicators[player.Index];
    }
	

    [SerializeField]
    private Indicator[] indicators;

    [SerializeField]
    private Slider timeSlider;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text messageText;

    [SerializeField]
    private HealthDisplay[] healthDisplays;

    private static GameDisplay instance;
}

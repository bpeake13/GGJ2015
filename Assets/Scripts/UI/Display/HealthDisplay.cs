using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    void Awake()
    {
        Transform t = transform;
        int childCount = t.childCount;
        healthContainers = new Toggle[childCount];
        for (int child = 0; child < childCount; child++)
        {
            healthContainers[child] = t.GetChild(child).GetComponent<Toggle>();
        }
    }

    void Update()
    {
        if (!boundController)
            return;

        int maxHealth = boundController.MaxHealth;
        int health = boundController.GetPlayerPiece().GetCurrentHealth();

        if (maxHealth != lastMaxHealth || health != lastHealth)
            Refresh();
    }

    public void BindPlayer(PlayerController player)
    {
        boundController = player;
        Refresh();
    }

    public void Refresh()
    {
        Transform t = transform;
        int childCount = healthContainers.Length;
        int child = 0;

        int maxHealth = boundController.MaxHealth;
        int health = boundController.GetPlayerPiece().GetCurrentHealth();

        for(child = 0; child < maxHealth && child < childCount; child++)
        {
            healthContainers[child].gameObject.SetActive(true);
        }

        for (; child < childCount; child++)
        {
            healthContainers[child].gameObject.SetActive(false);
        }

        for (child = 0; child < health && child < childCount; child++)
        {
            Toggle toggle = healthContainers[child];
            toggle.isOn = true;
        }

        for (; child < childCount; child++)
        {
            Toggle toggle = healthContainers[child];
            toggle.isOn = false;
        }

        lastHealth = health;
        lastMaxHealth = maxHealth;
    }

    private int currentActiveChildren;
    private ToggleGroup group;
    private PlayerController boundController;

    private Toggle[] healthContainers;

    private int lastHealth;
    private int lastMaxHealth;
}

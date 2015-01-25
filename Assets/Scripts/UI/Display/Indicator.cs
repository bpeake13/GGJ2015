using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Indicator : MonoBehaviour
{
    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void ShowActionItem()
    {
        actionItem.gameObject.SetActive(true);
        reactionItem.gameObject.SetActive(false);
        slowedItem.gameObject.SetActive(false);
        stunnedItem.gameObject.SetActive(false);
    }

    public void ShowReactionItem()
    {
        actionItem.gameObject.SetActive(false);
        reactionItem.gameObject.SetActive(true);
        slowedItem.gameObject.SetActive(false);
        stunnedItem.gameObject.SetActive(false);
    }

    public void ShowSlowedItem()
    {
        actionItem.gameObject.SetActive(false);
        reactionItem.gameObject.SetActive(false);
        slowedItem.gameObject.SetActive(true);
        stunnedItem.gameObject.SetActive(false);
    }

    public void ShowStunnedItem()
    {
        actionItem.gameObject.SetActive(false);
        reactionItem.gameObject.SetActive(false);
        slowedItem.gameObject.SetActive(false);
        stunnedItem.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!player)
            return;

        Player piece = player.GetPlayerPiece();
        Vector3 playerPosition = piece.GetVisual().transform.position;

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, playerPosition);

        Vector2 canvasPosition = Vector2.zero;;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, screenPosition, Camera.main, out canvasPosition);

        RectTransform t = GetComponent<RectTransform>();
        t.anchoredPosition = canvasPosition;

        Inventory inv = piece.GetInventory();
        IItem item1 = inv.GetItem(0);
        Sprite item1Spr;
        if (item1 != null)
            item1Spr = item1.GetDisplaySprite();
        else
            item1Spr = emptySprite;

        IItem item2 = inv.GetItem(1);
        Sprite item2Spr;
        if (item2 != null)
            item2Spr = item2.GetDisplaySprite();
        else
            item2Spr = emptySprite;

        for(int i = 0; i < item1Displays.Length; i++)
        {
            item1Displays[i].sprite = item1Spr;
        }

        for (int i = 0; i < item2Displays.Length; i++)
        {
            item2Displays[i].sprite = item2Spr;
        }
    }

    [SerializeField]
    private RectTransform actionItem;

    [SerializeField]
    private RectTransform reactionItem;

    [SerializeField]
    private RectTransform slowedItem;

    [SerializeField]
    private RectTransform stunnedItem;

    [SerializeField]
    private Image[] item1Displays;

    [SerializeField]
    private Image[] item2Displays;

    [SerializeField]
    private Sprite emptySprite;

    private PlayerController player;
}

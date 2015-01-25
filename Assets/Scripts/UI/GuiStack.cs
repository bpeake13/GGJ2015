using UnityEngine;
using System.Collections;

public class GuiStack : MonoBehaviour 
{
    public static GuiStack Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;

        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;

        sprites = new SpriteRenderer[maxSpriteCount];

        for(int i = 0; i < maxSpriteCount; i++)
        {
            GameObject newSpriteObject = new GameObject("spr_" + i, typeof(SpriteRenderer));
            newSpriteObject.SetActive(false);
            sprites[i] = newSpriteObject.GetComponent<SpriteRenderer>();
        }
    }

    public void Clear()
    {
        for(int i = 0; i < spriteIndex; i++)
        {
            sprites[i].gameObject.SetActive(false);
        }
    }

    public void Draw(Sprite sprite, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        int index = spriteIndex;
        spriteIndex++;
        if(spriteIndex > maxSpriteCount)
        {
            spriteIndex = 0;
        }

        SpriteRenderer sr = sprites[index];
        sr.gameObject.SetActive(true);

        sr.sprite = sprite;

        Transform t = sr.transform;

        t.localPosition = position;
        t.localRotation = rotation;
        t.localScale = scale;
    }

    public void Draw(Sprite sprite, Vector3 position, Quaternion rotation)
    {
        Draw(sprite, position, rotation, Vector3.one);
    }

    public void Draw(Sprite sprite, Vector3 position)
    {
        Draw(sprite, position, Quaternion.identity, Vector3.one);
    }

    public void DrawGrid(Sprite sprite, int gridX, int gridY, Quaternion rotation)
    {
        Vector3 loc = BoardGenerator.ConvertBoardSpaceToWorldSpace(gridX, gridY);
        loc.y += 0.5f;
        Draw(sprite, loc, Quaternion.Euler(90, rotation.eulerAngles.y, rotation.eulerAngles.z));
    }

    [SerializeField]
    private int maxSpriteCount = 64;

    private int spriteIndex = 0;
    private SpriteRenderer[] sprites;

    private static GuiStack instance;
}

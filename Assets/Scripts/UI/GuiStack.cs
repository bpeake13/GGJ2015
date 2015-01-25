using UnityEngine;
using System.Collections;

public class GuiStack : MonoBehaviour 
{
    void Awake()
    {
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

    void Update()
    {
        Clear();
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

    public void DrawGrid(Sprite sprite, int gridX, int gridY)
    {
        Vector3 loc = BoardGenerator.ConvertBoardSpaceToWorldSpace(gridX, gridY);
        Draw(sprite, loc);
    }

    [SerializeField]
    private int maxSpriteCount = 64;

    private int spriteIndex = 0;
    private SpriteRenderer[] sprites;
}

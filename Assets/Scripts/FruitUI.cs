using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitUI : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.sprite = null;
    }

    public void RenderSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHistory : MonoBehaviour
{

    private Queue<Sprite> history = new Queue<Sprite>();
    private List<FruitUI> fruitUIs = new List<FruitUI>();

    void Awake()
    {
        foreach (FruitUI child in GetComponentsInChildren<FruitUI>())
        {
            fruitUIs.Add(child);
        }
    }

    public void RenderFruitHistory()
    {
        int i = 0;
        foreach (Sprite fruitName in history)
        {
            fruitUIs[i].RenderSprite(fruitName);
            i++;
        }
    }

    public void AddFruit(Sprite fruit)
    {
        if (history.Count >= 7)
        {
            history.Dequeue();
            history.Enqueue(fruit);
        } else
        {
            history.Enqueue(fruit);
        }

        RenderFruitHistory();
    }

    public void EmptyHistory()
    {
        history.Clear();

        foreach (FruitUI child in GetComponentsInChildren<FruitUI>())
        {
            child.spriteRenderer.sprite = null;
        }
    }
}

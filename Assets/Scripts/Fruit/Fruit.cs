using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public int currentLevel = 0;
    public float initialCountdown;
    public float duration;

    public Sprite cherry;
    public Sprite strawberry;
    public Sprite orange;
    public Sprite apple;
    public Sprite melon;
    public Sprite galaxian;
    public Sprite bell;
    public Sprite key;

    public Dictionary<int, Sprite> sprites = new();
    public Dictionary<int, int> fruitScores = new();

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(false);

        sprites.Add(0, cherry);
        sprites.Add(1, strawberry);
        sprites.Add(2, orange);
        sprites.Add(3, apple);
        sprites.Add(4, melon);
        sprites.Add(5, galaxian);
        sprites.Add(6, bell);
        sprites.Add(7, key);

        fruitScores.Add(0, 100);
        fruitScores.Add(1, 300);
        fruitScores.Add(2, 500);
        fruitScores.Add(3, 700);
        fruitScores.Add(4, 1000);
        fruitScores.Add(5, 2000);
        fruitScores.Add(6, 3000);
        fruitScores.Add(7, 5000);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = sprites[currentLevel];
    }

    private void Eat()
    {
        FindAnyObjectByType<GameManager>().FruitEaten(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

}

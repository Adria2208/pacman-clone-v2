using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float currentTimeFloat = 0.0f;
    public int currentTime = 0;
    private bool waiting;

    private void Awake()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTimeFloat += Time.deltaTime;
        currentTime = Mathf.RoundToInt(currentTimeFloat);
    }

    public void Stop(float duration)
    {
        if (waiting)
        {
            return;
        }
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }

    public void InitialSpawnFruit(Fruit fruit)
    {
        StartCoroutine(FruitInitialCountdown(fruit));
    }

    IEnumerator FruitInitialCountdown(Fruit fruit)
    {
        yield return new WaitForSecondsRealtime(fruit.initialCountdown);
        fruit.Spawn();
    }

}

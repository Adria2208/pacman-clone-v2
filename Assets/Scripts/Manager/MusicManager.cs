using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void changeBGM(AudioClip bgm)
    {
        if (audioSource.clip != bgm)
        {
            audioSource.clip = bgm;
            StartBGM();
        }
    }

    public void StopBGM()
    {
       audioSource.Stop();
    }

    public void StartBGM()
    {
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bgAudioClip;

    [SerializeField] float timeToSwitch;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Play(bgAudioClip, true);
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if (musicToPlay == null) { return; }

        if (interrupt)
        {
            audioSource.volume = 1f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }
        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }

    }

    AudioClip switchTo;
    float volume;

    IEnumerator SmoothSwitchMusic()
    {
        volume = 1f;
        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] GameObject audioSourcePrefab;
    [SerializeField] int audioSourceCount;
    List<AudioSource> audioSources;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        audioSources = new List<AudioSource>();

        for (int i = 0; i < audioSourceCount; i++)
        {
            GameObject go = Instantiate(audioSourcePrefab, transform);
            go.transform.localPosition = Vector3.zero;
            audioSources.Add(go.GetComponent<AudioSource>());
        }
    }

    public void Play(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            Debug.LogError("Audio clip is null");
            return;
        }
        AudioSource audioSource = GetFreeAudioSource();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioSource GetFreeAudioSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                return audioSources[i];
            }
        }
        return audioSources[0];
    }
}

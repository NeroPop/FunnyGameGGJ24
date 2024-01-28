using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] NextPersonSources;
    [SerializeField] private AudioSource[] GetReadySources;
    [SerializeField] private AudioSource[] GameDialogueSources;
    [SerializeField] private AudioSource[] RandomSources;

    private void Start()
    {
        // Initialize your list of audio sources here if needed
        // audioSources = new AudioSource[] { audioSource1, audioSource2, audioSource3, ... };
    }

    public void PlayNextPerson()
    {
        if (NextPersonSources != null && NextPersonSources.Length > 0)
        {
            int randomIndex = Random.Range(0, NextPersonSources.Length);

            NextPersonSources[randomIndex].Play();
        }
        else
        {
            Debug.LogWarning("No NextPersonSources assigned to the list");
        }
    }

    public void PlayGetReady()
    {
        if (GetReadySources != null && GetReadySources.Length > 0)
        {
            int randomIndex = Random.Range(0, GetReadySources.Length);

            GetReadySources[randomIndex].Play();
        }
        else
        {
            Debug.LogWarning("No GetReadySources assigned to the list");
        }
    }

    public void PlayGameDialogue()
    {
        if (GameDialogueSources != null && GameDialogueSources.Length > 0)
        {
            int randomIndex = Random.Range(0, GameDialogueSources.Length);

            GameDialogueSources[randomIndex].Play();
        }
        else
        {
            Debug.LogWarning("No GameDialogueSources asassigned to the list");
        }
    }
    public void PlayRandom()
    {
        if (RandomSources != null && RandomSources.Length > 0)
        {
            int randomIndex = Random.Range(0, RandomSources.Length);

            RandomSources[randomIndex].Play();
        }
        else
        {
            Debug.LogWarning("No RandomSources asassigned to the list");
        }
    }
}

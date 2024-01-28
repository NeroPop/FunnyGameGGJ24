using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;

    private void Start()
    {
        // Initialize your list of audio sources here if needed
        // audioSources = new AudioSource[] { audioSource1, audioSource2, audioSource3, ... };
    }

    public void PlayAudio()
    {
        if (audioSources != null && audioSources.Length > 0)
        {
            int randomIndex = Random.Range(0, audioSources.Length);

            audioSources[randomIndex].Play();
        }
        else
        {
            Debug.LogWarning("No AudioSources assigned to the list");
        }
    }
}

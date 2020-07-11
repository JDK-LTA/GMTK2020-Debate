using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTimeShifter : MonoBehaviour
{
    AudioSource audioSource;
    AudioMixer mixer;

    [Range(0.5f, 2)]
    [SerializeField] float speedChange = 1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mixer = audioSource.outputAudioMixerGroup.audioMixer;

        audioSource.pitch = speedChange; 
        mixer.SetFloat("pitchBend", 1f / speedChange);
    }

    private void Update()
    {
        audioSource.pitch = speedChange;
        mixer.SetFloat("pitchBend", 1f / speedChange);
    }
}

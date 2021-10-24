using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void OnEnable()
    {
        SoundsUI.MusicVolumeChanged += OnMusicVolumeChanged;
    }

    private void OnDisable()
    {
        SoundsUI.MusicVolumeChanged -= OnMusicVolumeChanged;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefs.GetInt("music");
    } 

    private void OnMusicVolumeChanged(int volume)
    {
        _audioSource.volume = volume;
    }
}

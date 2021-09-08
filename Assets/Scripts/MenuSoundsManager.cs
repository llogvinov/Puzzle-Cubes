using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundsManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void OnEnable()
    {
        SoundsUI.SoundsVolumeChanged += OnSoundsVolumeChanged;
    }

    private void OnDisable()
    {
        SoundsUI.SoundsVolumeChanged -= OnSoundsVolumeChanged;
    }
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickedSound() => _audioSource.Play();
    
    private void OnSoundsVolumeChanged(int volume) => _audioSource.volume = volume;
}

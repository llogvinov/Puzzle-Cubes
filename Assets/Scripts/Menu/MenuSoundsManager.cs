using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundsManager : MonoBehaviour
{
    private AudioSource _audioSource;

    #region Singleton

    public static MenuSoundsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
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

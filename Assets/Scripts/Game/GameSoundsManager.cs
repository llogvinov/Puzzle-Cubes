using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundsManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [Header("Win clips")]
    [SerializeField] private AudioClip[] _audioClips;
    
    private void OnEnable()
    {
        SoundsUI.SoundsVolumeChanged += OnSoundsVolumeChanged;
        GameUI.PartsMatched += OnPartsMatched;
    }

    private void OnDisable()
    {
        SoundsUI.SoundsVolumeChanged -= OnSoundsVolumeChanged;
        GameUI.PartsMatched -= OnPartsMatched;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefs.GetInt("music");
    } 
    
    private void OnSoundsVolumeChanged(int volume)
    {
        _audioSource.volume = volume;
    }

    private void OnPartsMatched()
    {
        foreach (var audioClip in _audioClips)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundsManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _buttonClickClip;
    
    [Header("Swipe clips")] 
    [SerializeField] private AudioClip[] _swipeAudioClips;
    
    [Header("Win clips")]
    [SerializeField] private AudioClip[] _winAudioClips;

    #region Singleton

    public static GameSoundsManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
    private void OnEnable()
    {
        SoundsUI.SoundsVolumeChanged += OnSoundsVolumeChanged;
        PlayerInput.PartsMatched += OnPartsMatched;
    }

    private void OnDisable()
    {
        SoundsUI.SoundsVolumeChanged -= OnSoundsVolumeChanged;
        PlayerInput.PartsMatched -= OnPartsMatched;
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
        foreach (var audioClip in _winAudioClips)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlaySwipeSound()
    {
        _audioSource.PlayOneShot(RandomSwipeClip());
    }
    
    private AudioClip RandomSwipeClip() => _swipeAudioClips[Random.Range(0, _swipeAudioClips.Length)];

    public void PlayButtonSound()
    {
        _audioSource.PlayOneShot(_buttonClickClip);
    }
}

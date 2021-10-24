using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

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
        PlayerScrollInput.Swiped += PlaySwipeSound;
        WinCheckerScroll.PlayerWon += OnPlayerWon;
    }

    private void OnDisable()
    {
        PlayerScrollInput.Swiped -= PlaySwipeSound;
        WinCheckerScroll.PlayerWon -= OnPlayerWon;
    } 

    private void OnPlayerWon()
    {
        foreach (var audioClip in _winAudioClips)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }

    private void PlaySwipeSound()
    {
        var clip = RandomSwipeClip();

        if (clip != null)
            _audioSource.PlayOneShot(clip);
    }
    
    private AudioClip RandomSwipeClip() => _swipeAudioClips[Random.Range(0, _swipeAudioClips.Length)];

    public void PlayButtonSound() => _audioSource.PlayOneShot(_buttonClickClip);
}

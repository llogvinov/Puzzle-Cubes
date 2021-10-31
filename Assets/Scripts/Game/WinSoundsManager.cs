using System.Collections;
using UnityEngine;

public class WinSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _winAudioSource;
    [Header("Win clips")] 
    [SerializeField] private AudioClip _winSoundClip;
    [SerializeField] private AudioClip _winYeahClip;
    [SerializeField] private AudioClip _winApplauseClip;
    
    private void OnEnable()
    {
        WinCheckerScroll.PlayerWon += OnPlayerWon;
    }

    private void OnDisable()
    {
        WinCheckerScroll.PlayerWon -= OnPlayerWon;
    }
    
    private void OnPlayerWon()
    {
        if (_winAudioSource == null) 
            return;
        
        StartCoroutine(PlayWinSounds());
    }

    private IEnumerator PlayWinSounds()
    {
        _winAudioSource.PlayOneShot(_winSoundClip);
        _winAudioSource.PlayOneShot(_winYeahClip);
        yield return new WaitForSeconds(_winYeahClip.length / 2f);
        _winAudioSource.PlayOneShot(_winApplauseClip);
    }
}

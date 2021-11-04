using UnityEngine;

public class GameSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _gameAudioSource;

    [SerializeField] private AudioClip _buttonClickClip;
    
    [Header("Swipe clips")] 
    [SerializeField] private AudioClip[] _swipeAudioClips;
    
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
        GameScrollInput.Swiped += PlaySwipeSound;
    }

    private void OnDisable()
    {
        GameScrollInput.Swiped -= PlaySwipeSound;
    } 

    private void PlaySwipeSound()
    {
        if (_gameAudioSource == null) 
            return;
        
        _gameAudioSource.PlayOneShot(RandomSwipeClip());
    }
    
    private AudioClip RandomSwipeClip() => _swipeAudioClips[Random.Range(0, _swipeAudioClips.Length)];

    public void PlayButtonSound() => _gameAudioSource.PlayOneShot(_buttonClickClip);
}

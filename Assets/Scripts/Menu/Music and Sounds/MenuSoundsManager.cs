using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _buttonClickClip;
    [SerializeField] private AudioClip _swipeCategoriesClip;
    
    #region Singleton

    public static MenuSoundsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    private void PlayClip(AudioClip audioClip) => _audioSource.PlayOneShot(audioClip);
    
    public void PlayClickedSound() => PlayClip(_buttonClickClip);

    public void PlaySwipeSound() => PlayClip(_swipeCategoriesClip);
}

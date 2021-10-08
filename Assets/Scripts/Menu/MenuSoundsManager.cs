using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    #region Singleton

    public static MenuSoundsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
    public void PlayClickedSound() => _audioSource.Play();
}

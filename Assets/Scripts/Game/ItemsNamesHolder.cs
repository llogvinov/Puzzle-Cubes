using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsNamesHolder : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _enClips;
    [SerializeField] private List<AudioClip> _ruClips;
    [SerializeField] private List<AudioClip> _deClips;
    [SerializeField] private List<AudioClip> _frClips;
    [SerializeField] private List<AudioClip> _itClips;
    [SerializeField] private List<AudioClip> _esClips;
    [SerializeField] private List<AudioClip> _ptClips;
    [SerializeField] private List<AudioClip> _plClips;
    [SerializeField] private List<AudioClip> _cnClips;
    [SerializeField] private List<AudioClip> _jpClips;
    [SerializeField] private List<AudioClip> _krClips;

    private static Dictionary<SystemLanguage, List<AudioClip>> _clipsDictionary = new Dictionary<SystemLanguage, List<AudioClip>>();

    private void Start()
    {
        GenerateDictionary();
    }

    private void GenerateDictionary()
    {
        _clipsDictionary.Add(SystemLanguage.English, _enClips);
        _clipsDictionary.Add(SystemLanguage.Russian, _ruClips);
        _clipsDictionary.Add(SystemLanguage.German, _deClips);
        _clipsDictionary.Add(SystemLanguage.French, _frClips);
        _clipsDictionary.Add(SystemLanguage.Italian, _itClips);
        _clipsDictionary.Add(SystemLanguage.Spanish, _esClips);
        _clipsDictionary.Add(SystemLanguage.Portuguese, _ptClips);
        _clipsDictionary.Add(SystemLanguage.Polish, _plClips);
        _clipsDictionary.Add(SystemLanguage.ChineseSimplified, _cnClips);
        _clipsDictionary.Add(SystemLanguage.Japanese, _jpClips);
        _clipsDictionary.Add(SystemLanguage.Korean, _krClips);
    }

    public static AudioClip GetAudioClip(SystemLanguage language, string name)
    {
        var clip = _clipsDictionary[language].Find(x => x.name.Contains(name));

        return clip;
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AudioHolder
{
    private static List<AudioClip> _audioClips;
    
    public static AudioClip GetAudioClip(string name)
    {
        AudioClip audioClip = _audioClips.Find(x => x.name.Contains(name));
        
        return audioClip;
    }
    
    public static void SetAudioClipsList(SystemLanguage language)
    {
        _audioClips = language switch
        {
            SystemLanguage.English => LoadAudioClips("en"),
            SystemLanguage.Russian => LoadAudioClips("ru"),
            SystemLanguage.German => LoadAudioClips("de"),
            SystemLanguage.French => LoadAudioClips("fr"),
            SystemLanguage.Italian => LoadAudioClips("it"),
            SystemLanguage.Spanish => LoadAudioClips("es"),
            SystemLanguage.Portuguese => LoadAudioClips("pt"),
            SystemLanguage.Polish => LoadAudioClips("pl"),
            SystemLanguage.ChineseSimplified => LoadAudioClips("cn"),
            SystemLanguage.Japanese => LoadAudioClips("jp"),
            SystemLanguage.Korean => LoadAudioClips("kr"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static List<AudioClip> LoadAudioClips(string languageID)
    {
        return Resources.LoadAll<AudioClip>("SoundsNames/" + languageID).ToList();
    }
}

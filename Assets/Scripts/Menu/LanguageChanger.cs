using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanguageChanger : MonoBehaviour
{
    public static UnityAction LanguageChanged;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameDataManager.SetLanguage(SystemLanguage.Japanese);
            LanguageChanged.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            GameDataManager.SetLanguage(SystemLanguage.Russian);
            LanguageChanged.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            GameDataManager.SetLanguage(SystemLanguage.Korean);
            LanguageChanged.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameDataManager.SetLanguage(SystemLanguage.English);
            LanguageChanged.Invoke();
        }
    }
}

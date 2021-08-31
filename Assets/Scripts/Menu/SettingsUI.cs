using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _settingsBackground;
    [SerializeField] private Button _openSettings;
    [SerializeField] private Button _closeSettings;
    [Space]
    [SerializeField] private Button[] _buttonsToHide;
    [Space]
    [Header("Buttons on panel")] 
    [SerializeField] private Button _rateUs;
    [SerializeField] private Button _contactUs;
    [SerializeField] private Button _restorePurchases;
    [SerializeField] private Button _fullVersion;
    [SerializeField] private Button _ourWebsite;
    [SerializeField] private Button _privacyPolicy;

    private const string RateUs = "";
    private const string MailAddress = "";
    private const string Website = "";
    private const string PrivacyPolicy = "";
    
    private void Start()
    {
        AddButtonsEvents();
    }

    private void AddButtonsEvents()
    {
        _openSettings.onClick.RemoveAllListeners();
        _openSettings.onClick.AddListener(OpenSettingsUI);
        
        _closeSettings.onClick.RemoveAllListeners();
        _closeSettings.onClick.AddListener(CloseSettingsUI);
        
        _rateUs.onClick.RemoveAllListeners();
        _rateUs.onClick.AddListener(OpenRate);
        
        _contactUs.onClick.RemoveAllListeners();
        _contactUs.onClick.AddListener(OpenContact);
        
        _ourWebsite.onClick.RemoveAllListeners();
        _ourWebsite.onClick.AddListener(OpenWebsite);
        
        _privacyPolicy.onClick.RemoveAllListeners();
        _privacyPolicy.onClick.AddListener(OpenPrivacyPolicy);
    }

    private void OpenSettingsUI()
    {
        _settingsBackground.SetActive(true);
        _settingsPanel.SetActive(true);
        
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void CloseSettingsUI()
    {
        _settingsBackground.SetActive(false);
        _settingsPanel.SetActive(false);
        
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
    
    private void OpenRate() => Application.OpenURL(RateUs);
    
    private void OpenContact() => Application.OpenURL("mailto:" + MailAddress);

    private void OpenWebsite() => Application.OpenURL(Website);

    private void OpenPrivacyPolicy() => Application.OpenURL(PrivacyPolicy);

}

using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : PanelUI
{
    [SerializeField] private Button _openSettings;
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
    
    private void OnEnable() => CheckAgeUI.AgeConfirmed += OnAgeConfirmed;

    private void OnDisable() => CheckAgeUI.AgeConfirmed -= OnAgeConfirmed;
    
    private void Start() => AddButtonsEvents();

    private void AddButtonsEvents()
    {
        if (GameDataManager.GetFullVersionPurchased())
            _fullVersion.onClick.RemoveAllListeners();
        
        AddPanelButtonsEvents();
        
        _openSettings.onClick.RemoveAllListeners();
        _openSettings.onClick.AddListener(CheckAge);
        
        _rateUs.onClick.RemoveAllListeners();
        _rateUs.onClick.AddListener(OpenRate);
        
        _contactUs.onClick.RemoveAllListeners();
        _contactUs.onClick.AddListener(OpenContact);
        
        _ourWebsite.onClick.RemoveAllListeners();
        _ourWebsite.onClick.AddListener(OpenWebsite);
        
        _privacyPolicy.onClick.RemoveAllListeners();
        _privacyPolicy.onClick.AddListener(OpenPrivacyPolicy);
    }

    private void CheckAge()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        CheckAgeUI.Instance.OnCheckAge(PanelID);
    }
    
    private void OpenRate()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        Application.OpenURL(RateUs);
    }

    private void OpenContact()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        Application.OpenURL("mailto:" + MailAddress);
    }

    private void OpenWebsite()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        Application.OpenURL(Website);
    }

    private void OpenPrivacyPolicy()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        Application.OpenURL(PrivacyPolicy);
    }
}

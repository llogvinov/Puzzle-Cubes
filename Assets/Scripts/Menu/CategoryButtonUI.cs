using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CategoryButtonUI : MonoBehaviour
{
    [SerializeField] private Image _categoryImage;
    [SerializeField] private Text _categoryName;
    [SerializeField] private Shadow _darkShadow;
    [SerializeField] private Shadow _lightShadow;
    [SerializeField] private Image _lockImage;
    [Space] 
    [SerializeField] private float _defaultShadowValue;
    [SerializeField] private float _hieroglyphShadowValue;
    
    [HideInInspector] public int CategoryID;
    
    private Button _categoryButton;

    private void Awake()
    {
        _categoryButton = GetComponent<Button>();
    }

    public void SetInformation(Category category, SystemLanguage language, string key)
    {
        SetID(category);
        SetImage(category.Sprite);
        SetTitle(category, language, key);
        SetLock(category.IsLocked);
    }

    private void SetID(Category category)
    {
        CategoryID = category.ID;
    }
    
    private void SetImage(Sprite sprite) => _categoryImage.sprite = sprite;

    private void SetTitle(Category category, SystemLanguage language, string key)
    {
        SetName(language, key);
        SetColor(category);
        SetShadowThickness(language);
    }

    public void SetShadowThickness(SystemLanguage language)
    {
        if (language == SystemLanguage.ChineseSimplified ||
            language == SystemLanguage.Japanese ||
            language == SystemLanguage.Korean)
        {
            _darkShadow.effectDistance = new Vector2(0f, _hieroglyphShadowValue);
            _lightShadow.effectDistance = new Vector2(0f, -_hieroglyphShadowValue);
        }
        else
        {
            _darkShadow.effectDistance = new Vector2(0f, _defaultShadowValue);
            _lightShadow.effectDistance = new Vector2(0f, -_defaultShadowValue);
        }
    }

    public void SetName(SystemLanguage language, string key)
    {
        _categoryName.font = TextHolder.GetFont(language);
        _categoryName.text = TextHolder.GetTitle(language, key);
    }

    private void SetColor(Category category)
    {
        _categoryName.color = category.NameColor;
        _darkShadow.effectColor = category.DarkShadow;
        _lightShadow.effectColor = category.LightShadow;
    }
    public void SetLock(bool isLocked) => _lockImage.gameObject.SetActive(isLocked);
    
    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        _categoryButton.onClick.RemoveAllListeners();
        _categoryButton.onClick.AddListener(() => action?.Invoke(itemIndex));
    }

    public void OnTryPurchase(UnityAction action)
    {
        _categoryButton.onClick.RemoveAllListeners();
        _categoryButton.onClick.AddListener(() => action?.Invoke());
    }
}

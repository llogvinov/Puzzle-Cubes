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

    private Button _categoryButton;

    private void Awake()
    {
        _categoryButton = GetComponent<Button>();
    }

    public void SetInformation(Category category, SystemLanguage language, string key)
    {
        SetImage(category.Sprite);
        SetTitle(category, language, key);
        SetLock(category.IsLocked);
    }

    private void SetImage(Sprite sprite) => _categoryImage.sprite = sprite;

    private void SetTitle(Category category, SystemLanguage language, string key)
    {
        SetName(language, key);
        SetColor(category);
    }

    public void SetName(SystemLanguage language, string key)
    {
        _categoryName.font = TextHolder.GetCategoryFont();
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

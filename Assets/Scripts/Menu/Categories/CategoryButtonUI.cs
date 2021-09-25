using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CategoryButtonUI : MonoBehaviour
{
    [SerializeField] private Image _categoryImage;
    [SerializeField] private TMP_Text _categoryName;
    [SerializeField] private Image _lockImage;

    private Button _categoryButton;
    private PurchaseUI _purchaseUI;

    private void Awake()
    {
        _categoryButton = GetComponent<Button>();
        _purchaseUI = FindObjectOfType<PurchaseUI>();
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
        SetColor(category.NameColor);
    }

    public void SetName(SystemLanguage language, string key)
    {
        _categoryName.font = TextHolder.GetCategoryFont(language);
        _categoryName.text = TextHolder.GetTitle(language, key);
    }

    private void SetColor(Color color) => _categoryName.color = color;
    private void SetLock(bool isLocked) => _lockImage.gameObject.SetActive(isLocked);
    
    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        _categoryButton.onClick.RemoveAllListeners();
        _categoryButton.onClick.AddListener(() => action?.Invoke(itemIndex));
    }

    public void OnTryPurchase()
    {
        _categoryButton.onClick.RemoveAllListeners();
        _categoryButton.onClick.AddListener(() => CheckAgeUI.Instance.OnCheckAge(_purchaseUI.PanelID));
    }
}

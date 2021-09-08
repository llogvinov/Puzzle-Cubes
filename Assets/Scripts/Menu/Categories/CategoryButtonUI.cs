using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CategoryButtonUI : MonoBehaviour
{
    [SerializeField] private Image _categoryImage;
    [SerializeField] private Text _categoryName;
    
    [SerializeField] private Image _lockImage;

    private Button _categoryButton;
    private PurchaseUI _purchaseUI;

    private void Awake()
    {
        _categoryButton = GetComponent<Button>();
        _purchaseUI = FindObjectOfType<PurchaseUI>();
    }

    public void SetCategoryImage(Sprite sprite) => _categoryImage.sprite = sprite;

    public void SetCategoryName(string name) => _categoryName.text = name;

    public void SetCategoryLock(bool isLocked) => _lockImage.gameObject.SetActive(isLocked);
    
    // public void SetHatImageOpacity() => hatImage.color = new Color(0f, 0f, 0f, 0f);
    
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

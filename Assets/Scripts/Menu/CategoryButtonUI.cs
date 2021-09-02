using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CategoryButtonUI : MonoBehaviour
{
    [SerializeField] private Image _categoryImage;
    [SerializeField] private Text _categoryName;
    
    public void SetCategoryImage(Sprite sprite) => _categoryImage.sprite = sprite;

    public void SetCategoryName(string name) => _categoryName.text = name;
    
    // public void SetHatImageOpacity() => hatImage.color = new Color(0f, 0f, 0f, 0f);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoriesUI : MonoBehaviour
{
    [SerializeField] private CategoryDatabase _categoryDb;

    [SerializeField] private Button _categoryButton;
    [SerializeField] private Transform _buttonsContainer;

    [SerializeField] private Canvas _canvas;
    
    private void Awake()
    {
        ConfigureLayoutGroup();
        
        GenerateScrollView();
    }

    private void GenerateScrollView()
    {
        for (int i = 0; i < _categoryDb.GetLength(); i++)
        {
            Category category = _categoryDb.GetCategory(i);
            CategoryButtonUI categoryButtonUI =
                Instantiate(_categoryButton, _buttonsContainer).GetComponent<CategoryButtonUI>();

            // set button name in Hierarchy
            categoryButtonUI.gameObject.name = category.Name;
            
            // add information on button
            categoryButtonUI.SetCategoryImage(category.Sprite);
            categoryButtonUI.SetCategoryName(category.Name);

            if (!category.IsUnlocked)
            {
                // TODO: add logic for LOCKED categories
            }
        }
    }

    private void ConfigureLayoutGroup()
    {
        HorizontalLayoutGroup layoutGroup = _buttonsContainer.GetComponent<HorizontalLayoutGroup>();

        float canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;
        float buttonWidth = _categoryButton.GetComponent<RectTransform>().rect.width;
        
        Debug.Log("Canvas width - " + canvasWidth);
        Debug.Log("Button width - " + buttonWidth);
        Debug.Log((canvasWidth - buttonWidth) / 2f);
        
        ConfigurePaddingAndSpacing(layoutGroup, canvasWidth, buttonWidth);
    }
    
    private void ConfigurePaddingAndSpacing(HorizontalLayoutGroup layoutGroup, float canvasWidth, float buttonWidth)
    {
        layoutGroup.padding.left = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
        layoutGroup.padding.right = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
        layoutGroup.spacing = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
    }
}

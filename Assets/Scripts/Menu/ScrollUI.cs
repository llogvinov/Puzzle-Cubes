using UnityEngine;
using UnityEngine.UI;

public class ScrollUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _buttonsContainer;
    [SerializeField] private CategoryButtonUI _categoryButton;
    
    [SerializeField] private CategoryDatabase _categoryDatabase;
    
    [SerializeField] private PurchaseUI _purchaseUI;
    
    private void OnEnable()
    {
        Purchase.FullVersionPurchased += OnFullVersionPurchased;
        LanguageUI.LanguageChanged += TranslateCategories;
    }

    private void OnDisable()
    {
        Purchase.FullVersionPurchased -= OnFullVersionPurchased;
        LanguageUI.LanguageChanged -= TranslateCategories;
    }
    
    private void Start()
    {
        _categoryDatabase.CheckPurchase();
        
        ConfigureLayoutGroup();
        GenerateScrollUI();
    }

    private void GenerateScrollUI()
    {
        SystemLanguage language = GameDataManager.GetLanguage();
        int leftIndex;
        
        int selectedCategoryID = GameDataManager.GetSelectedCategoryID();
        int middleIndex = (_categoryDatabase.GetLength() - 1) / 2;
        
        if (selectedCategoryID < middleIndex)
            leftIndex = selectedCategoryID + middleIndex + 1;
        else
            leftIndex = selectedCategoryID - middleIndex;

        for (int i = leftIndex; i < _categoryDatabase.GetLength(); i++)
        {
            InstantiateButton(i, language);
        }

        for (int i = 0; i < leftIndex; i++)
        {
            InstantiateButton(i, language);
        }
    }

    private void InstantiateButton(int id, SystemLanguage language)
    {
        Category category = _categoryDatabase.GetCategory(id);
        CategoryButtonUI categoryButtonUI =
            Instantiate(_categoryButton, _buttonsContainer).GetComponent<CategoryButtonUI>();

        // set button name in Hierarchy
        categoryButtonUI.gameObject.name = "category" + id;
            
        // add information on button
        categoryButtonUI.SetInformation(category, language, categoryButtonUI.gameObject.name);
            
        if (!category.IsLocked)
            categoryButtonUI.OnItemSelect(id, OnItemSelected);
        else
            categoryButtonUI.OnTryPurchase(OnPurchaseTried);
    }
    
    private void OnItemSelected(int index)
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        // save data
        GameDataManager.SetSelectedCategory(_categoryDatabase.GetCategory(index), index);

        SceneControl.LoadGameScene();
    }

    private void OnPurchaseTried()
    {
        MenuSoundsManager.Instance.PlayClickedSound();

        CheckAgeUI.Instance.OnCheckAge(_purchaseUI.PanelID);
    }
    
    private void ConfigureLayoutGroup()
    {
        HorizontalLayoutGroup layoutGroup = _buttonsContainer.GetComponent<HorizontalLayoutGroup>();

        float canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;
        float buttonWidth = _categoryButton.GetComponent<RectTransform>().rect.width;

        ConfigurePaddingAndSpacing(layoutGroup, canvasWidth, buttonWidth);
    }
    
    private void ConfigurePaddingAndSpacing(HorizontalLayoutGroup layoutGroup, float canvasWidth, float buttonWidth)
    {
        layoutGroup.padding.left = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
        layoutGroup.padding.right = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
        layoutGroup.spacing = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
    }
    
    private void TranslateCategories(SystemLanguage language)
    {
        for (int i = 0; i < _buttonsContainer.transform.childCount; i++)
        {
            var categoryButtonUI = _buttonsContainer.transform.GetChild(i).GetComponent<CategoryButtonUI>();

            categoryButtonUI.SetName(language, categoryButtonUI.gameObject.name);
            categoryButtonUI.SetShadowValues(language);
        }
    }

    private void OnFullVersionPurchased()
    {
        _categoryDatabase.CheckPurchase();
        
        for (int i = 0; i < _buttonsContainer.transform.childCount; i++)
        {
            var categoryButtonUI = _buttonsContainer.transform.GetChild(i).GetComponent<CategoryButtonUI>();
            
            categoryButtonUI.SetLock(false);
            categoryButtonUI.OnItemSelect(categoryButtonUI.CategoryID, OnItemSelected);
        }
    }
}

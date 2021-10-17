using UnityEngine;
using UnityEngine.UI;

public class CategoriesUI : MonoBehaviour
{
    [SerializeField] private CategoryDatabase _categoryDb;

    [SerializeField] private Button _categoryButton;
    [SerializeField] private Transform _buttonsContainer;

    [SerializeField] private Canvas _canvas;
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
        _categoryDb.CheckPurchase();
        
        ClearContainer();
        ConfigureLayoutGroup();
        GenerateScrollView();
    }

    private void GenerateScrollView()
    {
        var lang = GameDataManager.GetLanguage();
        
        for (int i = 0; i < _categoryDb.GetLength(); i++)
        {
            Category category = _categoryDb.GetCategory(i);
            CategoryButtonUI categoryButtonUI =
                Instantiate(_categoryButton, _buttonsContainer).GetComponent<CategoryButtonUI>();

            // set button name in Hierarchy
            categoryButtonUI.gameObject.name = "category" + i;
            
            // add information on button
            categoryButtonUI.SetInformation(category, lang, categoryButtonUI.gameObject.name);
            
            if (!category.IsLocked)
            {
                categoryButtonUI.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                categoryButtonUI.OnTryPurchase(OnPurchaseTried);
            }
        }
    }
    
    private void OnItemSelected(int index)
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        // save data
        GameDataManager.SetSelectedCategory(_categoryDb.GetCategory(index), index);

        SceneControl.LoadGameScene();
    }

    private void OnPurchaseTried()
    {
        MenuSoundsManager.Instance.PlayClickedSound();

        CheckAgeUI.Instance.OnCheckAge(_purchaseUI.PanelID);
    }
    
    private void ClearContainer()
    {
        for (int i = 0; i < _buttonsContainer.childCount; i++)
        {
            Destroy(_buttonsContainer.GetChild(i).gameObject);
        }
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
        }
    }

    private void OnFullVersionPurchased()
    {
        _categoryDb.CheckPurchase();
        
        for (int i = 0; i < _buttonsContainer.transform.childCount; i++)
        {
            var categoryButtonUI = _buttonsContainer.transform.GetChild(i).GetComponent<CategoryButtonUI>();
            
            categoryButtonUI.SetLock(false);
            categoryButtonUI.OnItemSelect(i, OnItemSelected);
        }
    }
}

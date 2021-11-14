using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScrollUI : MonoBehaviour
{
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Image _itemBackgroundUI;
    [SerializeField] private Text _itemNameUI;
    [SerializeField] private Image _itemFullUI;
    [SerializeField] private Animator _itemAnimatorUI;
    [Space]
    [SerializeField] private GameScrollUI _gameScrollUI;
    [SerializeField] private AudioSource _audioSource;
    [Space]
    [SerializeField] private float _delay = 2f;
    [Header("Repeat Animation")]
    [SerializeField] private Button _repeatButton;
    [SerializeField] private float _animationDuration = 2f;

    private bool _canRepeatAnimation;
    private string _itemName;
    
    private void OnEnable()
    {
        WinCheckerScroll.ItemCollected += OnItemCollected;
        WinScrollInput.PanelSwiped += HideCanvas;
        
        _repeatButton.onClick.RemoveAllListeners();
        _repeatButton.onClick.AddListener(OnPanelClicked);
    }

    private void OnDisable()
    {
        WinCheckerScroll.ItemCollected -= OnItemCollected;
        WinScrollInput.PanelSwiped -= HideCanvas;
    }
    
    private void Start()
    {
        if (_winCanvas.gameObject.activeSelf)
            _winCanvas.gameObject.SetActive(false);
    }
    
    private void OnItemCollected(int id)
    {
        StartCoroutine(OnItemCollectedRoutine(id));
    }

    private IEnumerator OnItemCollectedRoutine(int id)
    {
        SetElementsUI(id);
        
        ShowCanvas();
        ShowAnimation();
        
        // SetFullItem(id);
        // ShowFullItem();
        
        if (_gameScrollUI != null)
            _gameScrollUI.GenerateUI();

        yield return new WaitForSeconds(_delay);
        ShowName();
        PlayNameClip(_itemName);
        yield return new WaitForSeconds(_delay);
        _canRepeatAnimation = true;
    }

    private void ShowCanvas()
    {
        _winCanvas.gameObject.SetActive(true);
    }

    private void SetElementsUI(int id)
    {
        _itemName = _gameScrollUI.ItemsDb.Items[id].Name;
        
        SetBackground(id);
        SetAnimation(id);
        SetFont(GameDataManager.GetLanguage());
        SetItemName(_itemName);
    }

    private IEnumerator AnimationCoolDown()
    {
        yield return new WaitForSeconds(_animationDuration);
        _canRepeatAnimation = true;
    }

    private void OnPanelClicked()
    {
        if (!_canRepeatAnimation)
            return;
        
        _canRepeatAnimation = false;
        
        ResetAnimation();
        PlayNameClip(_itemName);
        
        StartCoroutine(AnimationCoolDown());
    }
    
    private void SetBackground(int id)
    {
        _itemBackgroundUI.sprite = _gameScrollUI.ItemsDb.Items[id].Background;
    }
    
    private void SetAnimation(int id)
    {
        if (!_itemAnimatorUI.enabled)
            _itemAnimatorUI.enabled = true;

        var controller = _gameScrollUI.ItemsDb.Items[id].Controller;
        if (controller == null)
            return;
        
        _itemAnimatorUI.runtimeAnimatorController = controller.runtimeAnimatorController;
    }

    private void ResetAnimation()
    {
        var controller = _itemAnimatorUI.runtimeAnimatorController;
        if (controller == null)
            return;
        
        _itemAnimatorUI.runtimeAnimatorController = null;
        _itemAnimatorUI.runtimeAnimatorController = controller;
    }
    
    /*
    private void SetFullItem(int id)
    {
        var fullItemImage = _uiGenerator.ItemsDb.Items[id].FullItem;
        _fullItem.sprite = fullItemImage;
    }

    private void ShowFullItem()
    {
        _fullItem.gameObject.SetActive(true);
    }
    */
    
    private void ShowAnimation()
    {
        _itemAnimatorUI.gameObject.SetActive(true);
    }
    
    private void SetItemName(string itemName)
    {
        string itemNameText = TextHolder.GetName(GameDataManager.GetLanguage(), itemName);

        _itemNameUI.text = itemNameText;
    }

    private void SetFont(SystemLanguage language)
    {
        Font font = TextHolder.GetFont(language);
        if (_itemNameUI.font != font)
            _itemNameUI.font = font;
        
        // _itemNameUI.font = TextHolder.GetFont(language);
    }
    
    private void ShowName()
    {
        _itemNameUI.transform.localScale = Vector2.zero;
        _itemNameUI.gameObject.SetActive(true);
        _itemNameUI.transform.DOScale(Vector2.one, 0.4f);
    }

    private void PlayNameClip(string itemName)
    {
        _audioSource.PlayOneShot(SetItemClip(itemName));
    }
    
    private AudioClip SetItemClip(string itemName)
    {
        AudioClip clip = AudioHolder.GetAudioClip(itemName);

        return clip;
    }
    
    private void HideCanvas()
    {
        // _fullItem.gameObject.SetActive(false);
        _itemAnimatorUI.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(false);
        _itemNameUI.gameObject.SetActive(false);
    }
}
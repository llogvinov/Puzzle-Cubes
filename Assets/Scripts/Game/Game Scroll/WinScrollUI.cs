using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScrollUI : MonoBehaviour
{
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Image _background;
    [SerializeField] private Text _itemName;
    [SerializeField] private Image _fullItem;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private GenerateScrollUI _uiGenerator;
    [SerializeField] private AudioSource _audioSource;
    [Space]
    [SerializeField] private float _delay = 2f;

    private void OnEnable()
    {
        WinCheckerScroll.ItemCollected += OnItemCollected;
        WinScroll.PanelSwiped += HideCanvas;
    }

    private void OnDisable()
    {
        WinCheckerScroll.ItemCollected -= OnItemCollected;
        WinScroll.PanelSwiped -= HideCanvas;
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
        ShowCanvas();
        ShowBackground(id);
        SetAnimation(id);
        ShowAnimation();
        // SetFullItem(id);
        // ShowFullItem();
        
        if (_uiGenerator != null)
        {
            _uiGenerator.GenerateUI();
        }

        string itemName = _uiGenerator.ItemsDb.Items[id].Name;
        SetFont(GameDataManager.GetLanguage());
        SetItemName(itemName);

        yield return new WaitForSeconds(_delay);

        ShowName();
        
        yield return new WaitForSeconds(_delay / 2f);

        PlayNameClip(itemName);

        // yield return new WaitForSeconds(_delay * 2f);
        // HideCanvas();
    }

    private void ShowCanvas()
    {
        _winCanvas.gameObject.SetActive(true);
    }
    
    private void ShowBackground(int id)
    {
        _background.sprite = _uiGenerator.ItemsDb.Items[id].Background;
    }
    
    private void SetAnimation(int id)
    {
        if (!_animator.enabled)
            _animator.enabled = true;

        var controller = _uiGenerator.ItemsDb.Items[id].Controller;
        if (controller == null)
            return;
        
        _animator.runtimeAnimatorController = controller.runtimeAnimatorController;
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
        _animator.gameObject.SetActive(true);
    }
    
    private void SetItemName(string itemName)
    {
        string itemNameText = TextHolder.GetName(GameDataManager.GetLanguage(), itemName);

        _itemName.text = itemNameText;
    }

    private void SetFont(SystemLanguage language)
    {
        _itemName.font = TextHolder.GetFont(language);
    }
    
    private void ShowName()
    {
        _itemName.transform.localScale = Vector2.zero;
        _itemName.gameObject.SetActive(true);
        _itemName.transform.DOScale(Vector2.one, 0.4f);
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
        _animator.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(false);
        _itemName.gameObject.SetActive(false);
    }
}
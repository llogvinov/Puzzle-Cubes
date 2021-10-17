using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Image _background;
    [SerializeField] private Text _itemName;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private GameUIGenerator _uiGenerator;
    [SerializeField] private AudioSource _audioSource;
    [Space]
    [SerializeField] private float _delay = 2f;

    private void OnEnable()
    {
        WinChecker.ItemCollected += OnItemCollected;
        Win.PanelSwiped += HideCanvas;
    }

    private void OnDisable()
    {
        WinChecker.ItemCollected -= OnItemCollected;
        Win.PanelSwiped -= HideCanvas;
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
        
        _uiGenerator.GenerateUI();
        
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
        _animator.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(false);
        _itemName.gameObject.SetActive(false);
    }
}

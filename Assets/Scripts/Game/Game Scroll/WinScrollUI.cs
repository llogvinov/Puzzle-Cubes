﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScrollUI : MonoBehaviour
{
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Image _background;
    [SerializeField] private Text _itemNameUI;
    [SerializeField] private Image _fullItem;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private GenerateScrollUI _uiGenerator;
    [SerializeField] private AudioSource _audioSource;
    [Space]
    [SerializeField] private float _delay = 2f;

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
        
        if (_uiGenerator != null)
            _uiGenerator.GenerateUI();

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
        _itemName = _uiGenerator.ItemsDb.Items[id].Name;
        
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

    private void ResetAnimation()
    {
        var controller = _animator.runtimeAnimatorController;
        if (controller == null)
            return;
        
        _animator.runtimeAnimatorController = null;
        _animator.runtimeAnimatorController = controller;
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

        _itemNameUI.text = itemNameText;
    }

    private void SetFont(SystemLanguage language)
    {
        _itemNameUI.font = TextHolder.GetFont(language);
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
        _animator.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(false);
        _itemNameUI.gameObject.SetActive(false);
    }
}
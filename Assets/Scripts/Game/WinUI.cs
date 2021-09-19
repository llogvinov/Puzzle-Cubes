using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Text _itemName;

    [SerializeField] private GameUIGenerator _uiGenerator;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        PlayerInput.ItemCollected += OnItemCollected;
    }

    private void OnDisable()
    {
        PlayerInput.ItemCollected -= OnItemCollected;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnItemCollected(int id)
    {
        ShowBackground(id);
        StartCoroutine(ShowNameAndPlayAudio(id));
        
        _uiGenerator.GenerateUI();

        StartCoroutine(HidePanel());
    }

    private IEnumerator ShowNameAndPlayAudio(int id)
    {
        var itemName = _uiGenerator.ItemsDb.Items[id].Name;
        SetFont();
        SetItemName(itemName);
        
        yield return new WaitForSeconds(2f);

        _itemName.transform.localScale = Vector2.zero;
        _itemName.gameObject.SetActive(true);
        _itemName.transform.LeanScale(Vector2.one, 0.5f);
        
        StartCoroutine(PlayNameClip(itemName));
    }

    private void ShowBackground(int id)
    {
        _background.sprite = _uiGenerator.ItemsDb.Items[id].Background;
        _background.gameObject.SetActive(true);
    }

    private IEnumerator PlayNameClip(string itemName)
    {
        yield return new WaitForSeconds(1f);
        
        _audioSource.PlayOneShot(SetItemClip(itemName));
    }

    private void SetItemName(string itemName)
    {
        var lang = GameDataManager.GetLanguage();
        var itemNameText = TextHolder.GetName(lang, itemName);

        _itemName.text = itemNameText;
    }

    private void SetFont()
    {
        var lang = GameDataManager.GetLanguage();
        
        _itemName.font = TextHolder.GetFont(lang);
    }
    
    private AudioClip SetItemClip(string itemName)
    {
        var clip = AudioHolder.GetAudioClip(itemName);

        return clip;
    }
    
    // TODO: remove method
    private IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(5f);
        
        _background.gameObject.SetActive(false);
        _itemName.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Text _itemName;

    [SerializeField] private GameUIGenerator _uiGenerator;

    [SerializeField] private AudioSource _audioSource;

    private void OnEnable() => TestScript.ItemCollected += OnItemCollected;

    private void OnDisable() => TestScript.ItemCollected -= OnItemCollected;

    private void OnItemCollected(int id)
    {
        ShowBackground(id);
        StartCoroutine(ShowNameAndPlayAudio(id));
        
        _uiGenerator.GenerateUI();

        // TODO: remove this part
        StartCoroutine(HidePanel());
    }

    private IEnumerator ShowNameAndPlayAudio(int id)
    {
        string itemName = _uiGenerator.ItemsDb.Items[id].Name;
        
        SetFont(GameDataManager.GetLanguage());
        SetItemName(itemName);
        
        yield return new WaitForSeconds(2f);

        ShowName();
        StartCoroutine(PlayNameClip(itemName));
    }

    private void ShowName()
    {
        _itemName.transform.localScale = Vector2.zero;
        _itemName.gameObject.SetActive(true);
        _itemName.transform.DOScale(Vector2.one, 0.4f);
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
        string itemNameText = TextHolder.GetName(GameDataManager.GetLanguage(), itemName);

        _itemName.text = itemNameText;
    }

    private void SetFont(SystemLanguage language)
    {
        _itemName.font = TextHolder.GetFont(language);
    }
    
    private AudioClip SetItemClip(string itemName)
    {
        AudioClip clip = AudioHolder.GetAudioClip(itemName);

        return clip;
    }
    
    private IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(5f);
        
        _background.gameObject.SetActive(false);
        _itemName.gameObject.SetActive(false);
    }
}

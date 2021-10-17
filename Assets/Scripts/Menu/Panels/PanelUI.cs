using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    [SerializeField] protected GameObject _background;
    [SerializeField] protected GameObject _panelUI;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] protected Button[] _buttonsToHide;

    [SerializeField] private int _panelID;

    public int PanelID => _panelID;

    private const float LeanTime = 0.3f;
    
    protected void AddPanelButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }
    
    protected void OnAgeConfirmed()
    {
        if (_panelID != CheckAgeUI.CurrentPanelID)
            return;

        ShowPanel();
    }

    protected void ShowPanel()
    {
        _panelUI.SetActive(true);

        var panel = _panelUI.GetComponent<Transform>();
        panel.localPosition = new Vector2(0, Screen.height);
        panel.DOLocalMoveY(0f, LeanTime).SetEase(Ease.OutExpo);
    }

    protected void ShowBackground()
    {
        _background.SetActive(true);
        
        var background = _background.GetComponent<CanvasGroup>();
        background.alpha = 0;
        background.DOFade(1f, LeanTime);
    }

    protected void HideButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void ClosePanelUI()
    {
        MenuSoundsManager.Instance.PlayClickedSound();

        HidePanel();
        HideBackground();
        ShowButtons();
    }

    protected void HidePanel()
    {
        var panel = _panelUI.GetComponent<Transform>();
        
        panel
            .DOLocalMoveY(-Screen.height, LeanTime)
            .SetEase(Ease.InExpo)
            .OnComplete(() => _panelUI.SetActive(false));
    }

    protected void HideBackground()
    {
        var background = _background.GetComponent<CanvasGroup>();
        
        background
            .DOFade(1f, LeanTime)
            .OnComplete(() => _background.SetActive(false));
    }

    protected void ShowButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
}

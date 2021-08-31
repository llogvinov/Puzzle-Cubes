using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Scrollbar _scrollBar;

    [SerializeField] private float _lerpTime;
    
    private float _scrollPosition;
    private float[] _positions;

    private void Start()
    {
        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        
        float canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;
        float buttonWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        
        ConfigureLayoutGroup(layoutGroup, canvasWidth, buttonWidth);
    }

    private void Update()
    {
        _positions = new float[transform.childCount];
        float distance = 1f / (_positions.Length - 1f);

        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            _scrollPosition = _scrollBar.value;
        }
        else
        {
            foreach (var position in _positions)
            {
                if (_scrollPosition < position + (distance/2) && _scrollPosition>position - (distance/2))
                {
                    _scrollBar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(_scrollBar.value, position, _lerpTime);
                }
            }
        }

    }

    private void ConfigureLayoutGroup(HorizontalLayoutGroup layoutGroup, float canvasWidth, float buttonWidth)
    {
        layoutGroup.padding.left = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
        layoutGroup.padding.right = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
        layoutGroup.spacing = Mathf.RoundToInt((canvasWidth - buttonWidth) / 2f);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollBar;

    [SerializeField] private float _lerpTime;
    
    private float _scrollPosition;
    private float[] _positions;
    private float _distance;

    private void Start()
    {
        _positions = new float[transform.childCount];
        _distance = 1f / (_positions.Length - 1f);

        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _scrollPosition = _scrollBar.value;
        }
        else
        {
            foreach (var position in _positions)
            {
                if (_scrollPosition < position + (_distance/2) && _scrollPosition > position - (_distance/2))
                {
                    _scrollBar.value =
                        Mathf.Lerp(_scrollBar.value, position, _lerpTime);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            GameDataManager.UnpurchaseFullVersion();
            
            Debug.Log("full version purchased: " + GameDataManager.GetFullVersionPurchased());
        }
    }
}

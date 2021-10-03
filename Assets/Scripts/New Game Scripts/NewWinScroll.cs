using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewWinScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private Scrollbar _scrollbar;
    
    private float _scrollbarPosition;

    private float[] _positions;
    private float _distance;

    private bool _isPointerDown;
    private float t = 16;
    
    private void OnEnable()
    {
        NewMatchCheck.PartsMatched += OpenPanel;
        _scrollbar.value = 0f;
        _isPointerDown = true;
    }

    private void OnDisable()
    {
        NewMatchCheck.PartsMatched -= OpenPanel;
    }

    private void Start()
    {
        if (_scrollView.activeSelf)
            _scrollView.SetActive(false);

        _positions = new float[2];
        _distance = 1f / (_positions.Length - 1);
        
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
            Debug.Log(_positions[i]);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _scrollbarPosition = _scrollbar.value;
        }
        else
        {
            foreach (var position in _positions)
            {
                if (_scrollbarPosition > position - _distance * 0.9f &&
                    _scrollbarPosition < position + _distance * 0.9f)
                {
                    _scrollbar.value = Mathf.Lerp(_scrollbar.value, position, .2f);
                }
            }
        }
        
        if (Math.Abs(_scrollbar.value - 1f) < 0.01f)
            ClosePanel();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPointerDown)
            _isPointerDown = false;
        
        /*
        Debug.Log(_scrollbar.value);
        if (_scrollbar.value > 0)
        {
            // hide win ui panel
            ClosePanel();
        }
        */
    }

    private void ClosePanel()
    {
        Debug.Log("close");
        
        _scrollView.SetActive(false);
        _scrollbar.value = 0f;
    }

    private void OpenPanel(int id)
    {
        Debug.Log("open");
        
        _scrollView.SetActive(true);
    }
}

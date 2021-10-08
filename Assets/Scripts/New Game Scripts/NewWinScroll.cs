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

        CountPositions();
    }

    private void CountPositions()
    {
        _positions = new float[2];
        _distance = 1f / (_positions.Length - 1);
        
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
        
        if (_scrollbar.value > 0.1f)
            ClosePanel();
    }

    private void ClosePanel()
    {
        Debug.Log("close");
        
        _scrollbar.value = Mathf.Lerp(_scrollbar.value, 1f, 0.05f);
        
        _scrollView.SetActive(false);
    }

    private void OpenPanel(int id)
    {
        Debug.Log("open");
        
        _scrollbar.value = 0f;
        _scrollView.SetActive(true);
    }
}

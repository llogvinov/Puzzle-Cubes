using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _scrollbar.value = Mathf.Lerp(_scrollbar.value, _scrollbar.value + .25f, .02f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _scrollbar.value = Mathf.Lerp(_scrollbar.value, _scrollbar.value - .25f, .02f);
        }
    }
}

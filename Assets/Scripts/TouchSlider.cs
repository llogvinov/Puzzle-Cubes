using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction<float> OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction<float> OnPointerUpEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke(uiSlider.value);

        OnPointerDragEvent?.Invoke(uiSlider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        OnPointerDragEvent?.Invoke(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(uiSlider.value);

        //reset slider value
        uiSlider.value = 0f;
    }

    private void OnDestroy()
    {
        //remove listener
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}

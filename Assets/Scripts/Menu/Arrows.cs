using System;
using UnityEngine;
using DG.Tweening;

public class Arrows : MonoBehaviour
{
    [SerializeField] private Transform[] _arrows;

    [SerializeField] private float _duration;
    [SerializeField] private int _loopsNumber;
    [SerializeField] private float _finalScale;

    private float _t;
    private float _tMax;

    private void OnEnable()
    {
        MenuScrollInput.TutorialWatched += OnTutorialWatched;
    }

    private void OnDisable()
    {
        MenuScrollInput.TutorialWatched -= OnTutorialWatched;
    }

    private void OnTutorialWatched()
    {
        GameDataManager.TutorialWatched();
        gameObject.SetActive(false);
        foreach (var arrow in _arrows)
        {
            arrow.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (GameDataManager.GetTutorialWatched())
        {
            gameObject.SetActive(false);
            foreach (var arrow in _arrows)
            {
                arrow.gameObject.SetActive(false);
            }
        }
        
        _tMax = _duration * _loopsNumber + 4;
        _t = _tMax / 2f;
    }

    private void Update()
    {
        _t += Time.deltaTime;
        
        if (_t >= _tMax)
        {
            ShakeArrows();
            _t = 0f;
        }
    }
    
    private void ShakeArrows()
    {
        foreach (var arrow in _arrows)
        {
            arrow.DOScale(_finalScale, _duration).SetLoops(_loopsNumber * 2, LoopType.Yoyo);
        }
    }
}

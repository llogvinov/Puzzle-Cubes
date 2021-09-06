using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _maxLifeTime = 5f;

    private CloudPool _pool;
    
    private void OnEnable()
    {
        _pool = transform.parent.GetComponent<CloudPool>();
        
        _lifeTime = 0;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        _lifeTime += Time.deltaTime;
        if (_lifeTime > _maxLifeTime)
        {
            _pool.ReturnToPool(this);
        }
    }
}

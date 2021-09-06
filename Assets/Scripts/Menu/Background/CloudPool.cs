using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPool : MonoBehaviour
{
    [SerializeField] private Cloud _cloud;
    
    private Queue<Cloud> _clouds = new Queue<Cloud>();

    private void OnEnable()
    {
        AddClouds(5);
    }

    public Cloud GetCloud()
    {
        if (_clouds.Count == 0)
            AddClouds(1);
        
        return _clouds.Dequeue();
    }

    private void AddClouds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Cloud cloud = Instantiate(_cloud, transform);
            cloud.gameObject.SetActive(false);
            _clouds.Enqueue(cloud);
        }
    }

    public void ReturnToPool(Cloud cloud)
    {
        cloud.gameObject.SetActive(false);
        _clouds.Enqueue(cloud);
    }
    
    
}

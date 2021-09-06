using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSpawn;

    private float _spawnTime;
    private CloudPool _pool;

    private void Awake()
    {
        _pool = transform.GetChild(0).GetComponent<CloudPool>();

        _spawnTime = 0;
        Spawn();
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;

        if (!(_spawnTime >= _timeBetweenSpawn)) 
            return;
        
        _spawnTime = 0;
        Spawn();
    }

    private void Spawn()
    {
        var cloud = _pool.GetCloud();
        cloud.transform.position = transform.position;
        cloud.transform.rotation = transform.rotation;
        cloud.gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Map : MonoBehaviour
{
    public Config _config;

    private void Awake()
    {
    }

    private void Start()
    {
        _config.DifficultyChanged += DifficultyChanged;
        GenerateMap();
    }

    public void GenerateMap()
    {
        var configMapSize = _config._mapSize;
        if (Camera.main != null)
        {
            var main = Camera.main;
            main.orthographicSize = configMapSize.x * 0.5f + 1.5f;
            main.transform.position = new Vector3(configMapSize.x * 0.5f - 0.5f,
                configMapSize.y * 0.5f - 0.5f,
                -10);
        }

        var r = new Random();
        for (int i = 0; i < configMapSize.x; i++)
        {
            for (int j = 0; j < configMapSize.y; j++)
            {
                var cell = Instantiate(_config.pref, new Vector3(i, j),
                    Quaternion.identity);
                cell.isBomb = r.Next(0, 100) > 80;
            }
        }
    }

    private void DifficultyChanged(Difficulty obj)
    {
        
    }
}

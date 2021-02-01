using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public Vector2Int _mapSize;
    public Difficulty _difficulty = 0;
    public int _bombCount;
    public Cell pref;

    public Action<Difficulty> DifficultyChanged;

    private void Awake()
    {
        ChangeDifficulty(_difficulty);
    }

    public void ChangeDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                _bombCount = 10;
                _mapSize = new Vector2Int(10,10);
                break;
            case Difficulty.Normal:
                _bombCount = 20;
                _mapSize = new Vector2Int(15,15);
                break;
            case Difficulty.Hard:
                _bombCount = 35;
                _mapSize = new Vector2Int(20,20);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        DifficultyChanged?.Invoke(difficulty);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public GameState GameState { get; private set; }

    public event Action<GameState> OnGameStateChanged;
    public void SetGameState(GameState state)
    {
        GameState = state;
        OnGameStateChanged?.Invoke(GameState);
    }
    private void Start()
    {
        SetGameState(GameState.Loading);
    }
}

public enum GameState
{
    Loading,
    Idle,
    Gameplay,
    Win,
    Lose
}
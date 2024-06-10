using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GameState _gameState = GameState.Ready;
    public float _timer;
    private int _score;
    private float _currentSpeed = 0.95f;

    public int GetScore() { return _score; }
    public void AddScore() { 
        _score++;
        UIManager.ins.SetScoreText(_score);
    }
    public void InitScore() { _score = 0; }

    public float GetSpeed() 
    {
        if(_currentSpeed <= 3.0f) _currentSpeed += 0.05f;
        return _currentSpeed;
    }
}
public enum GameState
{
    Ready,
    Play,
    Over,
    End
}

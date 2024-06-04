using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GameState _gameState = GameState.Ready;
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
        if(_currentSpeed <= 2.5f) _currentSpeed += 0.05f;
        return _currentSpeed;
    }

}
public enum GameState
{
    Ready,
    Play,
    End
}

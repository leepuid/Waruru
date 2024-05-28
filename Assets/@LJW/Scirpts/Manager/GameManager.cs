using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GameState _gameState = GameState.Ready;
    private int _score;

    public int GetScore() { return _score; }
    public void AddScore() { 
        _score++;
        UIManager.ins.SetScoreText(_score);
    }
    public void InitScore() { _score = 0; }

}
public enum GameState
{
    Ready,
    Play,
    End
}

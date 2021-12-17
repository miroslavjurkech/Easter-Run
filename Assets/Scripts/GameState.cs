using System;

public class GameState
{
    private static readonly GameState Instance = new GameState();

    private int _collectedEggs;
    private int _livesLeft;
    private readonly DateTime _startedAt;
    private TimeSpan _time;

    private GameState()
    {
        _startedAt = DateTime.Now;
    }

    public static GameState GetInstance()
    {
        return Instance;
    }

    public int GetEggs()
    {
        return _collectedEggs;
    }

    public int GetLives()
    {
        return _livesLeft;
    }

    public DateTime GetStart()
    {
        return _startedAt;
    }

    public TimeSpan GetTime()
    {
        return _time;
    }

    public void SaveState(int eggs, int lives)
    {
        _collectedEggs = eggs;
        
        if (lives == 0)
        {
            _time = DateTime.Now.Subtract(_startedAt);
        }
        else
        {
            _livesLeft = lives;
        }
    }
}
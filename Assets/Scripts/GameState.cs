using System;
using Menu;

public class GameState
{
    private static readonly GameState Instance = new GameState();

    private int _collectedEggs;
    private int _livesLeft;
    private int _whippingNumber;
    private DateTime _startedAt;
    private TimeSpan _time;

    private GameState()
    {
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

    public int GetWhippingNumber()
    {
        return _whippingNumber;
    }

    public void ClearState()
    {
        ScoreManager.GetInstance().AddScore(new ScoreRecord(this));
        
        _collectedEggs = 0;
        _livesLeft = 0;
        _whippingNumber = 0;
        _time = TimeSpan.Zero;
    }

    public void NewGame()
    {
        _startedAt = DateTime.Now;
    }
    
    public void WhippingWin(int prize)
    {
        _collectedEggs += prize;
        _whippingNumber++;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private const string TOP_SCORE_PREFIX = "topScore.";
    private const int SCORES_SAVED = 10;

    private ScoreManager _instance = new ScoreManager();

    private ScoreManager()
    {

    }

    public ScoreManager GetInstance()
    {
        return _instance;
    }

    public void AddScore( ScoreRecord score )
    {
        var scores = GetTopScores();
        scores.Add(score);
        scores.Sort();
        scores.Reverse();
        if ( scores.Count > SCORES_SAVED )
        {
            scores.RemoveRange(SCORES_SAVED, scores.Count - SCORES_SAVED);
        }

        for ( int i = 0; i < SCORES_SAVED; ++i )
        {
            scores[i].Save(TOP_SCORE_PREFIX + i + '.');
        }
    }

    public List<ScoreRecord> GetTopScores() 
    {
        List<ScoreRecord> topScores = new List<ScoreRecord>();
        for (uint i = 0; i < SCORES_SAVED; ++i) 
        {
            ScoreRecord scoreRecord = new ScoreRecord(TOP_SCORE_PREFIX + i + '.');
            topScores.Add(scoreRecord);    
        }

        topScores.Sort();
        topScores.Reverse();

        return topScores;
    }

}

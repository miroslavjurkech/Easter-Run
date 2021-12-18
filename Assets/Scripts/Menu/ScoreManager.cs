using System.Collections.Generic;

namespace Menu
{
    public class ScoreManager
    {
        private const string TopScorePrefix = "topScore.";
        private const int ScoresSaved = 10;

        private static ScoreManager _instance = new ScoreManager();

        private ScoreManager()
        {
        }

        public static ScoreManager GetInstance()
        {
            return _instance;
        }

        public void AddScore(ScoreRecord score)
        {
            var scores = GetTopScores();
            
            scores.Add(score);
            scores.Sort();
            scores.Reverse();
            
            if (scores.Count > ScoresSaved)
            {
                scores.RemoveRange(ScoresSaved, scores.Count - ScoresSaved);
            }

            for (var i = 0; i < ScoresSaved; ++i)
            {
                scores[i].Save(TopScorePrefix + i + '.');
            }
        }

        public List<ScoreRecord> GetTopScores()
        {
            var topScores = new List<ScoreRecord>();
            for (uint i = 0; i < ScoresSaved; ++i)
            {
                var scoreRecord = new ScoreRecord(TopScorePrefix + i + '.');
                topScores.Add(scoreRecord);
            }

            topScores.Sort();
            topScores.Reverse();

            return topScores;
        }
    }
}
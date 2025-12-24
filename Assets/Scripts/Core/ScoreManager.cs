using UnityEngine;

namespace Core
{
    public static class ScoreManager
    {
        private const string HighScoreKey = "HighScore";

        public static void SetScore(int score)
        {
            var currentHighScore = GetScore();
            if (currentHighScore < score)
            {
                PlayerPrefs.SetInt(HighScoreKey, score);
            }
        }
        
        public static int GetScore() => PlayerPrefs.GetInt(HighScoreKey, 0);
    }
}
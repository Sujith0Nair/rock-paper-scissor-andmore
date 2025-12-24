using UnityEngine;

namespace Core
{
    public static class ScoreManager
    {
        private const string HIGH_SCORE_KEY = "HighScore";

        public static void SetScore(int score)
        {
            var currentHighScore = GetScore();
            if (currentHighScore < score)
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            }
        }
        
        public static int GetScore() => PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }
}
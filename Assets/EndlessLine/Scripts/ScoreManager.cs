using System.Collections;
using UnityEngine;

namespace EndlessLine
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }   
        public static event System.Action<int> OnScoreChanged;

        private int score = 0;
        private float scoreIncreaseInterval = 1f; // The interval (in seconds) to increase the score
        private int scoreIncreaseAmount = 10; // The amount to increase the score


        // Cached
        private GameplayManager gameplayManager;

        #region Properties
        public int Score { get { return score; } }
        #endregion


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            gameplayManager = GameplayManager.Instance;
        }


        public void StartCalculateScore()
        {
            StartCoroutine(IncreaseScoreCoroutine());
        }

        private IEnumerator IncreaseScoreCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(scoreIncreaseInterval);
                IncreaseScore();
            }
        }

        private void IncreaseScore()
        {
            if(gameplayManager.currentState == GameplayManager.GameState.PLAYING)
            {
                score += scoreIncreaseAmount;
                OnScoreChanged?.Invoke(score);
            }           
        }
    }
}

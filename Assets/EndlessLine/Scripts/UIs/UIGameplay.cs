using UnityEngine;
using TMPro;

namespace EndlessLine
{
    public class UIGameplay : EndlessLineCanvas
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable()
        {
            ScoreManager.OnScoreChanged += UpdateScoreText;
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreChanged -= UpdateScoreText;
        }

        private void Start()
        {
            UpdateScoreText(ScoreManager.Instance.Score);
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = $"{score}";
        }
    }
}

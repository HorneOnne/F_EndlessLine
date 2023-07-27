using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EndlessLine
{

    public class UIGameOver : EndlessLineCanvas
    {
        [SerializeField] private Button backgroundBtn;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Animator tapToRepeatAnim;

        private void OnEnable()
        {
            GameplayManager.OnGameOver += LoadScoreText;
        }

        private void OnDisable()
        {
            GameplayManager.OnGameOver -= LoadScoreText;
        }

        private void Start()
        {
            backgroundBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);
            });
        }

      
        private void OnDestroy()
        {
            backgroundBtn.onClick.RemoveAllListeners();
        }

        public override void DisplayCanvas(bool isDisplay)
        {
            base.DisplayCanvas(isDisplay);
            tapToRepeatAnim.enabled = isDisplay;
        }

        private void LoadScoreText()
        {
            scoreText.text = ScoreManager.Instance.Score.ToString();
        }
    }
}

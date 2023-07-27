using UnityEngine.SceneManagement;

namespace EndlessLine
{
    using UnityEngine;


    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;
        public static event System.Action OnPlaying;
        public static event System.Action OnWin;
        public static event System.Action OnGameOver;

        public enum GameState
        {
            WAITING,
            PLAYING,
            WIN,
            GAMEOVER,
            PAUSE,
        }


        [Header("Properties")]
        public GameState currentState;
        [SerializeField] private float waitTimeBeforePlaying = 0.5f;


        // Cached
        private UIManager uiManager;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1.0f;
        }

        private void OnEnable()
        {
            OnStateChanged += SwitchState;
        }

        private void OnDisable()
        {
            OnStateChanged -= SwitchState;
        }

        private void Start()
        {
            uiManager = UIManager.Instance;
            ChangeGameState(GameState.WAITING);       
        }

     

        public void ChangeGameState(GameState state)
        {
            currentState = state;
            OnStateChanged?.Invoke();
        }

        private void SwitchState()
        {
            switch(currentState)
            {
                default: break;
                case GameState.WAITING:

               
                    break;
                case GameState.PLAYING:
                    ScoreManager.Instance.StartCalculateScore();

                    uiManager.CloseAll();
                    uiManager.DisplayGameplayMenu(true);

                    OnPlaying?.Invoke();
                    break;
                case GameState.WIN:               
      
                    OnWin?.Invoke();
                    break;
                case GameState.GAMEOVER:
                    StartCoroutine(Utilities.WaitAfter(1.0f, () =>
                    {
                        uiManager.CloseAll();
                        uiManager.DisplayGameoverMenu(true);
                        SoundManager.Instance.PlaySound(SoundType.GameOver, false);
                    }));              
                    OnGameOver?.Invoke();
                    break;
                case GameState.PAUSE:
                    break;
            }
        }
    }       
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace EndlessLine
{
    public class Player : MonoBehaviour
    {
        public float speed = 5f; // Adjust this value to control the movement speed
        public float maxY = 5f; // Adjust this value to set the maximum vertical position
        public float minY = -5f; // Adjust this value to set the minimum vertical position

        public enum Side
        {
            Up,Down
        }

        private Rigidbody2D rb;
        public Side currentSide;

        // Cached
        private GameplayManager gameplayManager;
        private EventSystem eventSystem;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentSide = Side.Up;
        }

        private void Start()
        {
            gameplayManager = GameplayManager.Instance;
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }
        private void Update()
        {
            if(gameplayManager.currentState == GameplayManager.GameState.WAITING)
            {
                if(Input.GetMouseButtonDown(0) && eventSystem.IsPointerOverGameObject() == false)
                {
                    gameplayManager.ChangeGameState(GameplayManager.GameState.PLAYING);
                    return;
                }            
            }

            if(gameplayManager.currentState == GameplayManager.GameState.PLAYING)
            {
                if (Input.GetMouseButtonDown(0) && eventSystem.IsPointerOverGameObject() == false)
                {
                    switch (currentSide)
                    {
                        case Side.Up:
                            rb.MovePosition(new Vector2(rb.position.x, minY));
                            currentSide = Side.Down;
                            break;
                        default:
                            rb.MovePosition(new Vector2(rb.position.x, maxY));
                            currentSide = Side.Up;
                            break;
                    }

                    // Sound
                    StartCoroutine(Utilities.WaitAfter(0.05f, () =>
                    {
                        if(gameplayManager.currentState == GameplayManager.GameState.PLAYING)
                            SoundManager.Instance.PlaySound(SoundType.Tap, false);
                    }));
                    
                }
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Spike>() != null)
            {
                SoundManager.Instance.PlaySound(SoundType.Destroyed, false);
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);           
            }
        }
    }
}

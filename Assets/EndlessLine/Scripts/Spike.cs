using UnityEngine;


namespace EndlessLine
{
    public class Spike : MonoBehaviour
    {
        public static float xOffset = 1.85f;

        [SerializeField] private LayerMask wallLayer;
        private SpikeGenerator spikeGenerator;
        private Vector2 initPosition;
        
        public enum SpikeSide
        {
            Up, Down
        }

        public SpikeSide spikeSide;


        private void Start()
        {
            spikeGenerator = SpikeGenerator.Instance;
            switch (spikeSide)
            {
                case SpikeSide.Up:
                    initPosition = spikeGenerator.upsideSpawnPoint.transform.position;
                    break;
                default:
                    initPosition = spikeGenerator.downsideSpawnPoint.transform.position;
                    break;
            }      
        }

        private void Update()
        {
            transform.Translate(Vector2.left * spikeGenerator.currentSpeed * Time.deltaTime, Space.World);
        }


        public float GetMoveDistance()
        {
            return Vector2.Distance(initPosition, transform.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the collision is with the target layer
            if ((wallLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EndlessLine
{
    public class SpikeGenerator : MonoBehaviour
    {
        public static SpikeGenerator Instance;

        [SerializeField] private Spike upsideSpikePrefab;
        [SerializeField] private Spike downsideSpikePrefab;
        public Transform upsideSpawnPoint;
        public Transform downsideSpawnPoint;

        [Header("Properties")]
        public float minSpeed = 10;
        public float maxSpeed = 20;
        public float currentSpeed;
        public float timeToReachMaxSpeed = 300f;    // 5 minutes
        [Space(5)]
        public float distanceEachStage;
       


        // Cached
        private Spike lastSpawnSpike = null;
        private int minGroup = 2;
        private int maxGroup = 5;
        private Spike.SpikeSide side;
        private float elapsedTime;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            currentSpeed = minSpeed;
            elapsedTime = 0;
        }


        private void Update()
        {
            if (GameplayManager.Instance.currentState != GameplayManager.GameState.PLAYING) return;
            // Update the elapsed time
            elapsedTime += Time.deltaTime;
           

            if (lastSpawnSpike == null)
            {
                side = Utilities.GetRandomEnum<Spike.SpikeSide>();
                GenerateSpike(side, Vector2.zero);
                return;
            }
            
            if (lastSpawnSpike.GetMoveDistance() <= distanceEachStage) return;
            currentSpeed = CalculateSpeedIncrease(elapsedTime);

            side = Utilities.GetRandomEnum<Spike.SpikeSide>();
            bool isSpawnGroup = Random.Range(0f, 1f) < 0.5f;

            if (isSpawnGroup)
            {
                bool isContinuousSpawn = Random.Range(0, 2) == 0;
                int groupQuantity = Random.Range(minGroup, maxGroup);

                if (isContinuousSpawn)
                {
                    SpawnContinuousGroup(side, groupQuantity);
                }
                else
                {
                    float offsetX = Random.Range(1.0f, 2.0f);
                    SpawnDiscontinuousGroup(side, offsetX, groupQuantity);
                }
            }
            else
            {
                GenerateSpike(side, Vector3.zero);
            }
        }
  

        #region Spike Generate
        private void GenerateSpike(Spike.SpikeSide side, Vector3 offset)
        {
            switch (side)
            {
                default: break;
                case Spike.SpikeSide.Up:
                    lastSpawnSpike = Instantiate(upsideSpikePrefab, upsideSpawnPoint.position + offset, Quaternion.identity);
                    break;
                case Spike.SpikeSide.Down:
                    lastSpawnSpike = Instantiate(downsideSpikePrefab, downsideSpawnPoint.position + offset, Quaternion.Euler(0,0,180));
                    break;
            }         
        }
  
        private void SpawnContinuousGroup(Spike.SpikeSide side, int quantity)
        {
            if (side != Spike.SpikeSide.Up && side != Spike.SpikeSide.Down)
                return;

            for (int i = 0; i < quantity; i++)
            {
                Vector3 offset = new Vector2(i * Spike.xOffset, 0);
                GenerateSpike(side, offset);
            }
        }

        private void SpawnDiscontinuousGroup(Spike.SpikeSide side, float offsetX, int quantity)
        {
            if (side != Spike.SpikeSide.Up && side != Spike.SpikeSide.Down)
                return;

            for (int i = 0; i < quantity; i++)
            {
                Vector3 offset = new Vector2(i * Spike.xOffset * offsetX, 0);
                GenerateSpike(side, offset);
            }
        }
        #endregion

        #region difficulty
        private float CalculateSpeedIncrease(float elapsedTime)
        {
            // Calculate the percentage of time elapsed
            float timePercentage = Mathf.Clamp01(elapsedTime / timeToReachMaxSpeed);

            // Use the percentage to calculate the current speed within the range
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, timePercentage);

            return currentSpeed;
        }
        #endregion
    }
}
